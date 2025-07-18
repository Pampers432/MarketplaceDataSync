﻿using MarketplaceDataSync.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceDataSync.Data 
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; } = null!;
    }
}
