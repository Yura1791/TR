using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TR
{
    public partial class transportrouteContext : DbContext
    {
        public DbSet<Bus> Buses { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Runs> Runs { get; set; }
        public DbSet<Stoproute> Stoproutes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Connected MariyDB 10.6
            optionsBuilder.UseMySql("server=localhost;user=root;password=;database=transportroute;", new MySqlServerVersion(new Version(10, 6, 0)));
            //Connected MSSQL Server
            // optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB; Database=TestDB; Trusted_Connection=True");
        }
    }
}
