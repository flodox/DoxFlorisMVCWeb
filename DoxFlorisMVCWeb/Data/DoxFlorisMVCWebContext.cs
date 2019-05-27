using DoxFlorisMVCWeb.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoxFlorisMVCWeb.Data
{
    public class DoxFlorisMVCWebContext : DbContext
    {
        public DoxFlorisMVCWebContext(DbContextOptions<DoxFlorisMVCWebContext> options) : base(options) { }

        public DbSet<Bericht> Berichten { get; set; }
        public DbSet<Lid> Leden { get; set; }
        public DbSet<Speedrun> Speedruns { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bericht>().ToTable("Bericht").HasOne<Lid>(b => b.lid).WithMany(l => l.berichten).HasForeignKey(b => b.lidId);
            modelBuilder.Entity<Lid>().ToTable("Lid");
            modelBuilder.Entity<Speedrun>().ToTable("Speedrun").HasOne<Lid>(s => s.lid).WithMany(l => l.speedruns).HasForeignKey(s => s.lidId);
        }
    }
          
}
