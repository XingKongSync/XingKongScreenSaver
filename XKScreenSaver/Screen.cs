using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace XKScreenSaver
{
    public class Screen
    {
        private static Lazy<Screen> _instance = new Lazy<Screen>(() => new Screen());
        private const int CONST_TIME_WAIT = 2 * 60 * 1000;//2 minitues for auto turn off screen

        public static Screen Instance { get => _instance.Value; }

        private Screen() { }

        public ulong GetIdleTime()
        {
            ProcessStartInfo psi = new ProcessStartInfo("xprintidle");
            psi.RedirectStandardOutput = true;
            psi.UseShellExecute = false;
            Process proc = Process.Start(psi);
            string strIdleTime = proc.StandardOutput.ReadToEnd();
            return ulong.Parse(strIdleTime);
        }

        public void OnOffScreen(bool on)
        {
            int arg = on ? 1 : 0;//0: off 1: on
            Process.Start("vcgencmd", $"display_power {arg}");
        }

        public bool IsScreenActivated()
        {
            return GetIdleTime() < CONST_TIME_WAIT;
        }
    }
}
