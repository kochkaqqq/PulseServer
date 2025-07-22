using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Requests.Queries.GetDocument
{
	public class GetDocumentQuery : IRequest<Document>
	{
		public int DocumentId { get; set; }
	}
}
