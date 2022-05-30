using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorTicketSystem.Shared.ViewModels
{
    public class Earning
    {
        public Earning()
        {
            Id = new Guid();
        }
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Subject { get; set; }
        public EarningCategory Category { get; set; }
        public decimal Amount { get; set; }

    }
}
