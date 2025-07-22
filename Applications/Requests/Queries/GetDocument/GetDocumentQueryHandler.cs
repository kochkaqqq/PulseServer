using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Exceptions;

namespace Application.Requests.Queries.GetDocument
{
	public class GetDocumentQueryHandler : IRequestHandler<GetDocumentQuery, Document>
	{
		private readonly IDbContext _dbContext;

		public GetDocumentQueryHandler(IDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<Document> Handle(GetDocumentQuery request, CancellationToken cancellationToken)
		{
			var doc = await _dbContext.Documents.FirstOrDefaultAsync(d => d.DocumentId == request.DocumentId, cancellationToken) 
				?? throw new NotFoundException(nameof(Document), request.DocumentId.ToString());
			
			return doc;
		}
	}
}
