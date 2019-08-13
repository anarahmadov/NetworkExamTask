using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiClientChatLast.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string IPAddress { get; set; }
        public string PhoneNumber { get; set; }

        public User Clone()
        {
            User user = new User();
            this.Id = user.Id;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.EmailAddress = user.EmailAddress;
            return user;
        }

        public List<Conversation> Conversations { get; set; }
    }
}
