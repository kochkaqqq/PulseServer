using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Exceptions;
using Application.Converters;

namespace Application.Requests.Commands.UpdateRequestDocument
{
	public class UpdateRequestDocumentCommandHandler : IRequestHandler<UpdateRequestDocumentCommand, Request>
	{
		private readonly IDbContext _dbContext;

		public UpdateRequestDocumentCommandHandler(IDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<Request> Handle(UpdateRequestDocumentCommand request, CancellationToken cancellationToken)
		{
			var doc = new Document()
			{
				Title = request.Title,
				Extension = request.Extension,
				Hash = GetSHAHash.ComputeSha256Hash(request.File),
				File = request.File,
			};

			var req = await _dbContext.Requests
				.Include(r => r.Client)
				.Include(r => r.Manager)
				.Include(r => r.Document)
				.Include(r => r.Files)
				.FirstOrDefaultAsync(r => r.RequestId == request.RequestId, cancellationToken) ?? 
				throw new NotFoundException(nameof(Request), request.RequestId.ToString());

			var dbDoc = await _dbContext.Documents.FirstOrDefaultAsync(d => d.Hash == doc.Hash);

			if (dbDoc == null)
			{
				req.Document = doc;
			}
			else
			{
				req.Document = dbDoc;
			}
			
			await _dbContext.SaveChangesAsync(cancellationToken);

			return req;
		}
	}
}
