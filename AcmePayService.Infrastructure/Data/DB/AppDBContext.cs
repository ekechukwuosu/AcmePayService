using AcmePayService.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AcmePayService.Infrastructure.Data.DB
{
    public class AppDBContext : DbContext
    {
        public DbSet<Payment> Payments { get; set; }
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }
    }
}
