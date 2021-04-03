using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace XKScreenSaver
{
    public enum ScreenState
    {
        Unknown,
        On,
        Off
    }

    public class ScreenSaver
    {
   

        private Screen _screen;
        private Sensor _sensor;
        //private Thread _workerThread;
        //private volatile bool _canRun = false;

        private bool _isScreenActived = true;
        private bool _isSensorActived = true;
        private ScreenState _screenState = ScreenState.Unknown;

        public bool IsScreenActived 
        {
            get => _isScreenActived;
            set
            {
                if (_isScreenActived != value)
                {
                    _isScreenActived = value;
                    CheckScreenState();
                }
            }
        }

        public bool IsSensorActived
        {
            get => _isSensorActived;
            set
            {
                if (_isSensorActived != value)
                {
                    _isSensorActived = value;
                    CheckScreenState();
                }
            }
        }

        private ScreenState ScreenState
        {
            get => _screenState;
            set
            {
                if (value != _screenState)
                {
                    _screenState = value;
                    switch (_screenState)
                    {
                        case ScreenState.On:
                            _screen.OnOffScreen(true);
                            break;
                        case ScreenState.Off:
                            _screen.OnOffScreen(false);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public ScreenSaver(Screen screen, Sensor sensor)
        {
            _sensor = sensor;
            _screen = screen;
        }

        //public void Start()
        //{
        //    _canRun = true;
        //    _workerThread = new Thread(WorkerThreadHandler);
        //    _workerThread.IsBackground = true;
        //    _workerThread.Start();
        //}

        //public void Stop()
        //{
        //    _canRun = false;
        //    _workerThread?.Join();
        //}

        //private void WorkerThreadHandler()
        //{
        //    while (_canRun)
        //    {
        //        IsScreenActived = _screen.IsScreenActivated();
        //        IsSensorActived = _sensor.IsSensorTriggered();
        //        Thread.Sleep(500);
        //    }
        //}

        public void DoHeartBeat()
        {
            IsScreenActived = _screen.IsScreenActivated();
            IsSensorActived = _sensor.IsSensorTriggered();
        }

        private void CheckScreenState()
        {
            if (IsScreenActived || IsSensorActived)
            {
                ScreenState = ScreenState.On;
            }
            else
            {
                ScreenState = ScreenState.Off;
            }
        }
    }
}
