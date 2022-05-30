using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorTicketSystem.Shared.ViewModels;

namespace BlazorTicketSystem.Shared.Models
{
    public class EarningModel
    {
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [MinLength(5)]
        public string Subject { get; set; }
        [Required]
        public EarningCategory Category { get; set; }
        [Required]
        public decimal Amount { get; set; }

    }
}
