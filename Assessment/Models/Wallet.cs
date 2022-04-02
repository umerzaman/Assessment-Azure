using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Models
{
    public class Wallet
    {
        public int WalletId { get; set; }
        public int UserId { get; set; }
        public double Balance { get; set; }
    }
}
