using Microsoft.EntityFrameworkCore;
using MyHome.WebAPI.Models;
using System.Collections.Generic;

namespace MyHome.WebAPI.Context
{
    public class AppDBContext : DbContext
    {
        public AppDBContext()
        {
        }

        public AppDBContext(DbContextOptions<AppDBContext> options)
           : base(options)
        {

        }
        public DbSet<TestEntity> tblTest { get; set; }

    }
}
