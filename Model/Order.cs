using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Order
    {
        public int ItemId { get; set; }
        public string Uemail { get; set; }
        public string VisaNumber { get;set; }
        public int Qnty { get; set; }
        public int Price { get; set; }
        public string OrderDate { get; set; }
        public string OrderStatus { get; set; }

    }
}
