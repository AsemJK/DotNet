using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorTicketSystem.Shared.ViewModels
{
    public class Ticket
    {
		public int Id { get; set; } // int
		public string ToDoSubject { get; set; } // longtext
		public string TeamMember { get; set; } // varchar(255)
		public DateTime CreationDate { get; set; } // datetime
		public string LastStatus { get; set; } // varchar(255)
		public decimal CompanyId { get; set; } // decimal(10,0)
		public string ImageFileName { get; set; }
		public DateTime LastUpdate { get; set; } // datetime
        public string TenantName { get; set; }
        public int TenantId { get; set; }
        public List<Tenant> Tenants { get; set; }
    }
}
