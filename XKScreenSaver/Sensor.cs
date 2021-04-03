using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Text;

namespace XKScreenSaver
{
    public class Sensor
    {
        public enum Status
        {
            Inactivated,
            Activated
        }

        private static Lazy<Sensor> _instance = new Lazy<Sensor>(() => new Sensor());
        private GpioController _gpio;
        private const int CONST_SENSOR_PIN = 37;//board pin index of gpio sensor

        private bool _relayTrigger = false;
        private DateTime _relayBeginTime;
        private int _relayTrueCount = 0;

        public bool RelayTrigger 
        { 
            get => _relayTrigger;
            set
            {
                if (_relayTrigger != value)
                {
                    if (value)
                    {
                        if (_relayTrueCount > 3)
                        {
                            _relayBeginTime = DateTime.Now;
                            _relayTrigger = true;
                            _relayTrueCount = 0;
                        }
                        else
                        {
                            _relayTrueCount++;
                        }
                    }
                    else
                    {
                        if (_relayBeginTime.AddSeconds(30) < DateTime.Now)
                        {
                            _relayTrigger = false;
                        }
                    }
                }
            }
        }


        public static Sensor Instance { get => _instance.Value; }

        //public event Action<Status> SensorStatusChanged;

        private Sensor() 
        {
            _gpio = new GpioController(PinNumberingScheme.Board);
            _gpio.OpenPin(CONST_SENSOR_PIN);
            _gpio.SetPinMode(CONST_SENSOR_PIN, PinMode.Input);
            //_gpio.RegisterCallbackForPinValueChangedEvent(CONST_SENSOR_PIN, PinEventTypes.Rising, SensorValueChanged);
            //_gpio.RegisterCallbackForPinValueChangedEvent(CONST_SENSOR_PIN, PinEventTypes.Falling, SensorValueChanged);
        }

        public bool IsSensorTriggered()
        {
            bool realTrigger = _gpio.Read(CONST_SENSOR_PIN).Equals(PinValue.High);
            RelayTrigger = realTrigger;
            Console.WriteLine($"RealTrigger: {realTrigger}, RelayTrigger: {RelayTrigger}");
            return RelayTrigger;
        }

        //private void SensorValueChanged(object sender, PinValueChangedEventArgs pinValueChangedEventArgs)
        //{
        //    switch (pinValueChangedEventArgs.ChangeType)
        //    {
        //        case PinEventTypes.None:
        //            break;
        //        case PinEventTypes.Rising:
        //            SensorStatusChanged?.Invoke(Status.Activated);
        //            break;
        //        case PinEventTypes.Falling:
        //            SensorStatusChanged?.Invoke(Status.Inactivated);
        //            break;
        //        default:
        //            break;
        //    }
        //}

    }
}
