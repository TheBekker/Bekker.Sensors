using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Gpio;

namespace Bekker.Sensors.Ping
{
    public class Pinger
    {
        private readonly GpioPin _echoPin;
        private readonly GpioPin _trigPin;
        private double _distance;

        public Pinger(GpioPin echoPin, GpioPin trigPin)
        {
            _echoPin = echoPin;
            _trigPin = trigPin;

            _echoPin.SetDriveMode(GpioPinDriveMode.Input);
            _trigPin.SetDriveMode(GpioPinDriveMode.Output);
            _trigPin.Write(GpioPinValue.Low);
        }

        public async Task<double> GetDistance()
        {
            var sw = new Stopwatch();
            var sw_timeout = new Stopwatch();
            sw_timeout.Start();

            _trigPin.Write(GpioPinValue.High);
            await Task.Delay(1);
            _trigPin.Write(GpioPinValue.Low);

            while (_echoPin.Read() != GpioPinValue.High)
            {
                if (sw_timeout.ElapsedMilliseconds > 500)
                    return 3.5;
            }
            sw.Start();

            while (_echoPin.Read() == GpioPinValue.High)
            {
                if (sw_timeout.ElapsedMilliseconds > 500)
                    return 3.4;
            }
            sw.Stop();
            
            // multiply by speed of sound in milliseconds (34000) divided by 2 (cause pulse make rountrip)
            var distance = (sw.Elapsed.TotalMilliseconds * 1000) * 17000;
            return distance;
        }
    }
}
