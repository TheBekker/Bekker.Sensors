using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Bekker.Sensors.CMPS10;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Example
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static CMPS10 Cmps;
        public MainPage()
        {
            var tries = 0;
            var maxTries = 3;
            this.InitializeComponent();
            Unloaded += MainPage_Unloaded;
            Cmps = new CMPS10();
            Cmps.Init();
            var timer = new Stopwatch();
            timer.Start();
            while (true)
            {
                if (Cmps._isInited)
                {
                    break;
                }
                else
                {
                    if (timer.ElapsedMilliseconds > 10000)
                    {
                        Debug.WriteLine("timeout...");
                        tries++;
                        if (tries > maxTries)
                        {
                            Debug.WriteLine("max tries...");
                            throw new Exception("No connection to i2c");
                        }
                        else
                        {
                            Cmps.ReInit();
                            Debug.WriteLine("reinit...");
                            timer.Restart();
                        }
                        
                    }
                }

            }
            var timer2 = new System.Threading.Timer((e) =>
            {
                Debug.WriteLine(Cmps.GetBearing());
            }, null, 0, 1000);
            var test = "";

        }

        private void MainPage_Unloaded(object sender, RoutedEventArgs e)
        {
            Cmps.Disconnect();
        }
    }
}
