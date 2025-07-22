using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Objects.Queries.GetObjectDetails
{
    public class GetObjectDetailsQuery : IRequest<Domain.Client>
    {
        public int Id { get; set; }
    }
}
