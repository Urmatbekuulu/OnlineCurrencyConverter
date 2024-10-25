using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Backend.Data_Access_Layer.Entity
{
    [PrimaryKey(nameof(CurrencyCode), nameof(ActualDate))]
    public class Currency 
    {
        [MaxLength(5), MinLength(1), Required]
        public string CurrencyCode { get; set; } = null!;
        public string? CurrencyName { get; set; }
        public decimal BuyRateToBaseCurrency { get; set; }
        public decimal SellRateToBaseCurrency { get; set; }
        [Required]
        public DateOnly ActualDate { get; set; }
    }
}
