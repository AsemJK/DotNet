using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorTicketSystem.Shared.ViewModels
{
    public class User
    {
		public decimal UserId { get; set; } // decimal(18,0)
		public string UserName { get; set; } // longtext
		public string Password { get; set; } // longtext
		public sbyte? Active { get; set; } // tinyint
		public decimal? RoleId { get; set; } // decimal(18,0)
		public string Narration { get; set; } // longtext
		public DateTime? ExtraDate { get; set; } // datetime
		public string Extra1 { get; set; } // longtext
		public string Extra2 { get; set; } // longtext
		public decimal? EmployeeId { get; set; } // decimal(18,0)
		public decimal CompanyId { get; set; } // decimal(18,0)
		public string ApiKey { get; set; } // varchar(150)
		public DateTime? ApiExpiryDate { get; set; } // datetime
	}
}
