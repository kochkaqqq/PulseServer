using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reports.Queries.GetReportList
{
    public class GetReportListQuery : IRequest<List<Report>>
    {
		public int WorkerId { get; set; } = 0;
    }
}
