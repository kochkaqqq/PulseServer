using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Exceptions;
using Domain;

namespace Application.Requests.Commands.RemoveRequestFile
{
	public class RemoveRequestFileCommandHandler : IRequestHandler<RemoveRequestFileCommand, Unit>
	{
		private readonly IDbContext _dbContext;

		public RemoveRequestFileCommandHandler(IDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<Unit> Handle(RemoveRequestFileCommand request, CancellationToken cancellationToken)
		{
			var req = await _dbContext.Requests.FirstOrDefaultAsync(r => r.RequestId == request.RequestId) 
				?? throw new NotFoundException(nameof(Request), request.RequestId.ToString());

			if (req.Files == null)
				throw new Exception(nameof(Request.Files));

			var file = req.Files.FirstOrDefault(f => f.DocumentId == request.DocumentId)
				?? throw new NotFoundException(nameof(Document), request.DocumentId.ToString());

			req.Files.Remove(file);

			await _dbContext.SaveChangesAsync(cancellationToken);

			return Unit.Value;
		}
	}
}
