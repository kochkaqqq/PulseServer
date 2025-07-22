using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Exceptions;
using Domain;

namespace Application.Requests.Commands.RemoveRequestDocument
{
	public class RemoveRequestDocumentCommandHandler : IRequestHandler<RemoveRequestDocumentCommand, Unit>
	{
		private readonly IDbContext _dbContext;

		public RemoveRequestDocumentCommandHandler(IDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<Unit> Handle(RemoveRequestDocumentCommand request, CancellationToken cancellationToken)
		{
			var req = await _dbContext.Requests.FirstOrDefaultAsync(r => r.RequestId == request.RequestId, cancellationToken)
				?? throw new NotFoundException(nameof(Request), request.RequestId.ToString());

			req.Document = null;
			req.DocumentId = null;

			await _dbContext.SaveChangesAsync(cancellationToken);

			return Unit.Value;
		}
	}
}
