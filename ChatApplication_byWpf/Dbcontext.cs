using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data.Entity.Migrations;


namespace ChatApplication_byWpf
{
    internal class MyDbContext : DbContext
    {
        public MyDbContext() : base("name=MyDbContext")
        {
            Database.Connection.ConnectionString = "Server=DESKTOP-3FB40M6\\SQLEXPRESS;Database=chattDatabase;Trusted_Connection=True;Trusted_Connection=True;MultipleActiveResultSets=True;";

        }
    

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        
        

    internal sealed class Configuration : DbMigrationsConfiguration<MyDbContext>
    {
        public Configuration()
        {
                AutomaticMigrationsEnabled = false ;
        }

        protected override void Seed(MyDbContext context)
        {
           
        }
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
        }
    }
}
