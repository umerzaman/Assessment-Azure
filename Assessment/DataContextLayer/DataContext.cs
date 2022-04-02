using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assessment.Models;

namespace Assessment.DataContecxt
{
    public  class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
        : base(options)
        { 
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
