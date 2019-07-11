using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiClientChatLast.CustomEvents
{
    public class MyEventArgs : EventArgs
    {
        Type nextUserControlType;

        public MyEventArgs(Type Type)
        {
            nextUserControlType = Type;
        }

        public Type GetTypeOfNext()
        {
            return nextUserControlType;
        }
    }
}
