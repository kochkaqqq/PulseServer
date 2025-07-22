using MediatR;
using Application.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using Application.Exceptions;
using Application.Converters;

namespace Application.Requests.Commands.CreateRequest
{
    public class CreateRequestCommandHandler : IRequestHandler<CreateRequestCommand, Request>
    {
        private readonly IDbContext _dbContext;

        public CreateRequestCommandHandler(IDbContext dbContext) => _dbContext = dbContext;

        public async Task<Request> Handle(CreateRequestCommand request, CancellationToken cancellationToken)
        {
            var client = await _dbContext.Clients.FirstOrDefaultAsync(c => c.ClientId == request.ClientId) ?? 
                throw new NotFoundException(nameof(Client), request.ClientId.ToString());
            var manager = await _dbContext.Managers.FirstOrDefaultAsync(m => m.ManagerId == request.ManagerId) ??
                throw new NotFoundException(nameof(Manager), request.ManagerId.ToString());

            var entity = new Request()
            {
                Client = client,
                Date = request.Date,
                ReasonRequest = request.ReasonRequest ?? string.Empty,
                NecessaryFunds = request.NecessaryFunds ?? string.Empty,
                Manager = manager,
                InternalInfo = request.InternalInfo ?? string.Empty,
                Status = request.Status,
                WorkResultType = request.WorkResultType,
                ActFilePath = request.ActFilePath ?? string.Empty,
                Experiencies = new List<Experience>()
            };

			if (request.Document != null)
			{
				request.Document.Hash = GetSHAHash.ComputeSha256Hash(request.Document.File);
				var dbDoc = await _dbContext.Documents.FirstOrDefaultAsync(d => d.Hash == request.Document.Hash);
				if (dbDoc != null)
				{
					entity.Document = dbDoc;
				}
				else
				{
					entity.Document = request.Document;
				}
			}

			if (request.Files != null && request.Files.Count > 0)
			{
				entity.Files = new List<Document>();
				foreach (var file in request.Files)
				{
					file.Hash = GetSHAHash.ComputeSha256Hash(file.File);
					var dbDoc = await _dbContext.Documents.FirstOrDefaultAsync(d => d.Hash == file.Hash);
					if (dbDoc != null)
					{
						entity.Files.Add(dbDoc);
					}
					else
					{
						entity.Files.Add(file);
					}
				}
			}

            await _dbContext.Requests.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity;
        }
    }
}
