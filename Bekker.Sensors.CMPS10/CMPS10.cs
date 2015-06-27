using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.I2c;
using Windows.Foundation;

namespace Bekker.Sensors.CMPS10
{
    public class CMPS10
    {
        public static string I2CControllerName;
        public static byte I2CAddress;
        public bool _isInited = false;

        private I2cDevice _device;

        public CMPS10(byte i2CAddress = 0x60, string controllerName = "I2C1")
        {
            I2CAddress = i2CAddress;
            I2CControllerName = controllerName;
            
        }

        public void Disconnect()
        {
            _device?.Dispose();
        }

        public void ReInit()
        {
            _device?.Dispose();
            Init();
        }

        public async void Init()
        {
            try
            {
                string aqs = I2cDevice.GetDeviceSelector();                     /* Get a selector string that will return all I2C controllers on the system */
                var dis = await DeviceInformation.FindAllAsync(aqs);            /* Find the I2C bus controller device with our selector string           */

                var settings = new I2cConnectionSettings(I2CAddress)
                {
                    BusSpeed = I2cBusSpeed.FastMode
                };
                
                _device = await I2cDevice.FromIdAsync(dis[0].Id, settings);
                if (_device == null)
                {
                    
                }
                else
                {
                    _isInited = true;
                }
            }
            catch
            {
                _isInited = false;
                throw;
            }
        }

        public double GetBearing()
        {
            return ReadWord();
        }

        private double ReadWord()
        {
            var readBuffer = new byte[2];
            _device.WriteRead(new byte[] { 2,3 }, readBuffer);
            int high = readBuffer[0] << 8;
            int low = readBuffer[1];
            return (high + low) / 10d;
        }
    }
}
