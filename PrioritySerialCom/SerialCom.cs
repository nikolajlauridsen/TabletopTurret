using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;

using Priority_Queue;

namespace PrioritySerialCom
{
    public class SerialCom
    {
        public bool Running { get; private set; }
        public int CheckDelay = 5;

        private SimplePriorityQueue<PriorityMessage> _msgQueue = new SimplePriorityQueue<PriorityMessage>();
        private SerialPort _port;

        public SerialCom(string portName, int baud, int readTimeout, int writeTimeout)
        {
            _port = new SerialPort(portName, baud, Parity.None);
            _port.ReadTimeout = readTimeout;
            _port.WriteTimeout = writeTimeout;
        }

        public SerialCom(string portName, int baud) : this(portName, baud, 100, 100)
        {
            
        }

        public void Write(PriorityMessage msg)
        {
            _msgQueue.Enqueue(msg, msg.Priority);
        }

        public void Start()
        {
            Running = true;
            Thread writerThread = new Thread(writeWorker);
            writerThread.IsBackground = true;
            writerThread.Start();
        }

        public void Stop()
        {
            Running = false;
        }

        private void writeWorker()
        {
            _port.Open();
            while (Running) {
                if (_msgQueue.Count > 0) {
                    PriorityMessage msg = _msgQueue.Dequeue();
                    _port.Write(msg.Message);
                    Thread.Sleep(msg.WaitTime);
                } else {
                    Thread.Sleep(CheckDelay);
                }
            }
            _port.Close();
        }



    }
}
