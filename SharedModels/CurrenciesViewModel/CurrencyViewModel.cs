using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.CurrenciesViewModel
{
    public class CurrencyViewModel
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
