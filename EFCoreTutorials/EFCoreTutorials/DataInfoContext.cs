using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace EFCoreTutorials
{
    public class DataInfoContext : DbContext
    {
        public DbSet<DataInfo> DInfo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB; Database=DataInfoDB;Trusted_Connection=True;");
        }
    }
}
