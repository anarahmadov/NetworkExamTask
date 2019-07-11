using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiClientChatLast.Domain
{
    public class Conversation
    {
        public int Id { get; set; }
        public User ToUser { get; set; }

        public List<Message> Messages { get; set; }
    }
}
