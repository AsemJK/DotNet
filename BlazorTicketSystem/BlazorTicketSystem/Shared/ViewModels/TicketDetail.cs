using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorTicketSystem.Shared.ViewModels
{
    public class TicketDetail
    {
		public int Id { get; set; } // int
		public int ToDoId { get; set; } // int
		public int Slno { get; set; } // int
		public string TeamMember { get; set; } // varchar(255)
		public string Notes { get; set; } // longtext
		public DateTime ExtraDate { get; set; }
	}
}
