using AcmePayService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AcmePayService.Infrastructure.DB
{
    public class AppDBContext : DbContext
    {
        public DbSet<Payment> Payments { get; set; }
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDBContext).Assembly);
        }
    }
}
