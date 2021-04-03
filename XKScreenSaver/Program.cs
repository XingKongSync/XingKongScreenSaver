using System;
using System.Device.Gpio;
using System.Diagnostics;
using System.Threading;

namespace XKScreenSaver
{

    class Program
    {
        //const int CONST_TIME_WAIT = 2 * 60 * 1000;//2 minitues for auto turn off screen
        //const int CONST_SENSOR_PIN = 37;//board pin index of gpio sensor

        static void Main(string[] args)
        {
            Environment.SetEnvironmentVariable("DISPLAY", ":0");

            ScreenSaver screenSaver = new ScreenSaver(Screen.Instance, Sensor.Instance);
            screenSaver.DoHeartBeat();
            while (true)
            {
                screenSaver.DoHeartBeat();
                Thread.Sleep(500);
            }
            //if (args == null || args.Length == 0)
            //{
            //    DoScreenSave();
            //}
            //else if (args.Length == 1)
            //{
            //    switch (args[0])
            //    {
            //        case "-t":
            //            Test();
            //            break;
            //        case "-t1":
            //            TestSensor();
            //            break;
            //        default:
            //            break;
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("Please ensure xprintidle has installed on your system.");
            //    Console.WriteLine("Usage: .\\XKScreenSaver -t To test if dependency has installed properly.");
            //}
        }

        #region Sreen

        //static void Test()
        //{
        //    try
        //    {
        //        ulong idleTime = GetIdleTime();
        //        Console.WriteLine($"Idle: {idleTime / 1000}s");

        //        Console.WriteLine("It seems ok.");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error: " + ex.Message);
        //        Console.WriteLine("Please check if you have installed xprintidle");
        //    }
        //}

        //static void DoScreenSave()
        //{
        //    ScreenState screenState = ScreenState.Unknown;
        //    while (true)
        //    {
        //        try
        //        {
        //            ulong idleTime = GetIdleTime();
        //            //Console.WriteLine($"Idle: {idleTime / 1000}s");
        //            if (idleTime > CONST_TIME_WAIT)
        //            {
        //                if (screenState != ScreenState.Off)
        //                {
        //                    OnOffScreen(false);
        //                    screenState = ScreenState.Off;
        //                }
        //            }
        //            else
        //            {
        //                if (screenState != ScreenState.On)
        //                {
        //                    OnOffScreen(true);
        //                    screenState = ScreenState.On;
        //                }
        //            }

        //            Thread.Sleep(500);
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine("Error: " + ex.Message);
        //        }
        //    }
        //}

        //static ulong GetIdleTime()
        //{
        //    ProcessStartInfo psi = new ProcessStartInfo("xprintidle");
        //    psi.RedirectStandardOutput = true;
        //    psi.UseShellExecute = false;
        //    Process proc = Process.Start(psi);
        //    string strIdleTime = proc.StandardOutput.ReadToEnd();
        //    return ulong.Parse(strIdleTime);
        //}

        //static void OnOffScreen(bool on)
        //{
        //    int arg = on ? 1 : 0;//0: off 1: on
        //    Process.Start("vcgencmd", $"display_power {arg}");
        //}
        #endregion

        #region Sensor
        //static void TestSensor()
        //{
        //    Console.WriteLine("Reading gpio input from pin " + CONST_SENSOR_PIN);

        //    GpioController gpio = new GpioController(PinNumberingScheme.Board);
        //    gpio.OpenPin(CONST_SENSOR_PIN);
        //    gpio.SetPinMode(CONST_SENSOR_PIN, PinMode.Input);

        //    while (true)
        //    {
        //        PinValue val = gpio.Read(CONST_SENSOR_PIN);

        //        Console.WriteLine(val.Equals(PinValue.High) ? "1" : "0");

        //        Thread.Sleep(1000);
        //    }
        //}
        #endregion
    }
}
