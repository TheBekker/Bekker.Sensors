using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Gpio;

namespace Bekker.Sensors.Pinger
{
    public class Pinger
    {
        private readonly GpioPin _pin;
        private readonly Stopwatch _pulseLength = new Stopwatch();
        private long startOfPulseAt;
        private long endOfPulse;
        public int Distance;
        public Pinger(int pin)
        {
            _pin = GpioController.GetDefault().OpenPin(pin);
            _pin.SetDriveMode(GpioPinDriveMode.Output);
            _pin.Write(GpioPinValue.Low);
        }

        public void Enable()
        {
            _pin.ValueChanged += _pin_ValueChanged;
        }
        public void Disable()
        {
            _pin.ValueChanged += null;
        }

        public void GetRange()
        {
            var mre = new ManualResetEvent(false);
            _pin.Write(GpioPinValue.High);
            mre.WaitOne(TimeSpan.FromMilliseconds(0.01));
            _pin.Write(GpioPinValue.Low);
        }

        private void _pin_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs args)
        {
            if(args.Edge == GpioPinEdge.RisingEdge)
            {
                Debug.WriteLine("startpulse");
                startOfPulseAt = System.DateTime.Now.Ticks;
            }

            if (args.Edge == GpioPinEdge.FallingEdge)
            {
                Debug.WriteLine("endpulse");
                endOfPulse = System.DateTime.Now.Ticks;

                if(startOfPulseAt > 0)
                {
                    var ticks = (int)(endOfPulse - startOfPulseAt);
                    Distance = ticks / 580;
                    Debug.WriteLine(Distance);
                }
            }
        }
    }
}
