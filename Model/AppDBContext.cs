﻿using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace BloodDonationSystem.Model
{
    public class AppDBContext : DbContext
    {
        public AppDBContext() : base()
        { 
        
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            var configSection = configBuilder.GetSection("ConnectionStrings");

            var connectionString = configSection["Hbdonation"];

            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Donar>().Property(x => x.DonarId).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Donar>()
            .HasOne(d => d.Inventory)
            .WithMany(i => i.DonarList)
            .HasForeignKey(d => d.BloodGroup)
            .HasPrincipalKey(i => i.BloodGroup);
        }

        public DbSet<Donar> Donar { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
    }

    
}
