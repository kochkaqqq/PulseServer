using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Reports
{
	public class ReportListDTO
	{
		public int ReportId { get; set; }
		public Worker Worker { get; set; } = null!;
		public DateTime Date { get; set; }
		public int MediaCount { get; set; }
		public bool IsWorkDone { get; set; }
	}
}
