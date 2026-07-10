using CleanMind.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanMind.Persistence;

public class CleanMindDbContext : DbContext
    {
    public CleanMindDbContext ( DbContextOptions<CleanMindDbContext> options )
    : base(options)
        {

        }

    protected CleanMindDbContext ()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CleanMindDbContext).Assembly);
        }

        public DbSet<Clinic> Clinics { get; set; }
    }

