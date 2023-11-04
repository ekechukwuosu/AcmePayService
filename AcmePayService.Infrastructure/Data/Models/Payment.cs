using AcmePayService.Infrastructure.Data.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcmePayService.Infrastructure.Data.Models
{
    [Index(nameof(OrderReference), IsUnique = true)]
    public class Payment
    {
        [Key]
        public int Tid { get; set; }
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        [MaxLength(3)]
        public string Currency { get; set; }
        public string CardHolderNumber { get; set; }
        public string HolderName { get; set; }
        public int ExpirationMonth { get; set; }
        public int ExpirationYear { get; set;}
        public int CVV { get; set; }
        [StringLength(50)]
        public string OrderReference { get; set; }        
        public string Status { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateUpdated { get; set; }
    }   
}
