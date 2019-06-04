using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrioritySerialCom
{
    public class PriorityMessage
    {
        public string Message;
        public int Priority;
        public int WaitTime;

        public PriorityMessage(string message, int priority, int waitTime)
        {
            Message = message;
            Priority = priority;
            WaitTime = waitTime;
        }

        public PriorityMessage(string message, int priority) : this(message, priority, 60)
        {

        }
    }
}
