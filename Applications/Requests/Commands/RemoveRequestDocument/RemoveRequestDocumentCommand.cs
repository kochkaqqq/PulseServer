using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Requests.Commands.RemoveRequestDocument
{
	public class RemoveRequestDocumentCommand : IRequest<Unit>
	{
		public int RequestId { get; set; }
	}
}
