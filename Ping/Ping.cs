using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Bekker.Sensors.Ping
{
    public class Ping
    {
        //private readonly GpioController _gpio = GpioController.GetDefault();
        //private readonly GpioPin _pin;
        //private readonly Stopwatch _pulseLength = new Stopwatch();
        public Ping(int pin)
        {
            //_pin = _gpio.OpenPin(pin);
            //_pin.SetDriveMode(GpioPinDriveMode.Output);
            //_pin.Write(GpioPinValue.Low);
        }

        //public async Task<double> GetRange()
        //{
        //    _pulseLength.Reset();

        //    _pin.Write(GpioPinValue.High);
        //    await Task.Delay(TimeSpan.FromMilliseconds(0.1));
        //    _pin.Write(GpioPinValue.Low);
            
            
        //    _pin.SetDriveMode(GpioPinDriveMode.Input);
        //    _pulseLength.Start();
        //    while (_pin.Read() == GpioPinValue.High)
        //    {
        //    }
        //    _pulseLength.Stop();

        //    var timeBetween = _pulseLength.Elapsed;
        //    Debug.WriteLine(timeBetween.ToString());

        //    return timeBetween.TotalSeconds * 17000;
        //}
    }
}
