using Application.Converters;
using Application.Exceptions;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Requests.Commands.AddRequestFile
{
	public class AddRequestFileCommandHandler : IRequestHandler<AddRequestFileCommand, Document>
	{
		private readonly IDbContext _dbContext;

		public AddRequestFileCommandHandler(IDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<Document> Handle(AddRequestFileCommand request, CancellationToken cancellationToken)
		{
			var req = await _dbContext.Requests.Include(r => r.Files).FirstOrDefaultAsync(r => r.RequestId == request.RequestId, cancellationToken) 
				?? throw new NotFoundException(nameof(Request), request.RequestId.ToString());

			var document = new Document()
			{
				Title = request.Title,
				Extension = request.Extension,
				File = request.File,
				Hash = GetSHAHash.ComputeSha256Hash(request.File)
			};

			var dbDoc = await _dbContext.Documents.FirstOrDefaultAsync(d => d.Hash == document.Hash, cancellationToken);

			if (req.Files == null)
				req.Files = new List<Document>();

			if (dbDoc == null)
			{
				req.Files.Add(document);
				await _dbContext.SaveChangesAsync(cancellationToken);
				document.File = default;
				return document;
			}
			else
			{
				req.Files.Add(dbDoc);
				await _dbContext.SaveChangesAsync(cancellationToken);
				dbDoc.File = default;
				return dbDoc;
			}
		}
	}
}
