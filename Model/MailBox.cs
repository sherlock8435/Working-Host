using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class MailBox
    {
        public string  SenderEmail { get; set; }
        public string  msgDate { get; set; }
        public string  SenderLName { get; set; }
        public string  SenderFName { get; set; }
        public string  msgSubject { get; set; }
        public string  msgBody { get; set; }
        public bool msgRead { get; set; }
        public string  RecieverEmail { get; set; }

    }
}
