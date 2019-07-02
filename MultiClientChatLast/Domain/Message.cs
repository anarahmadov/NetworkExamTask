using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiClientChatLast.Domain
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string MessageDetails { get; set; }
    }
}
