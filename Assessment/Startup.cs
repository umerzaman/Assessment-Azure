using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assessment.DataContecxt;
using Microsoft.EntityFrameworkCore;

[assembly: FunctionsStartup(typeof(Assessment.Startup))]
namespace Assessment
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            string connectionString = Environment.GetEnvironmentVariable("SqlConnectionString");
            builder.Services.AddDbContext<DataContext>(
                options => SqlServerDbContextOptionsExtensions.UseSqlServer(options, connectionString));
        }
    }
}
