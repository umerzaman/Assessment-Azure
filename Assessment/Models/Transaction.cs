using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int UserId { get; set; }
        public string Direction { get; set; }
        public double Amount { get; set; }
        public string Account { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
