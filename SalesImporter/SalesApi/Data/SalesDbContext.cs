using Microsoft.EntityFrameworkCore;
using SalesApi.Models;
using System.Collections.Generic;

namespace SalesApi.Data
{
    public class SalesDbContext : DbContext
    {
        public SalesDbContext(DbContextOptions<SalesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Sale> Sales { get; set; }
    }
}