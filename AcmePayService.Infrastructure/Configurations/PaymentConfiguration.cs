using AcmePayService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcmePayService.Infrastructure.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(x => x.Tid);
            builder.Property(a => a.Currency)
                .HasMaxLength(3)
                .IsRequired();
            builder.Property(a => a.OrderReference)
                .HasMaxLength(50);
            builder.HasIndex(x => x.OrderReference)
                .IsUnique(true);
        }
    }
}
