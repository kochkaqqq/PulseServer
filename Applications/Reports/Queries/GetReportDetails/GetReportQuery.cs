using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reports.Queries.GetReportDetails
{
    public class GetReportQuery : IRequest<Report>
    {
        public int Id { get; set; }
    }
}
