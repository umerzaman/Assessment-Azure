﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Dto
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public string Direction { get; set; }
        public int Account { get; set; }
    }
}
