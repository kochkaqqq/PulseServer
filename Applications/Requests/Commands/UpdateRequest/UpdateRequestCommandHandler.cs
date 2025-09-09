using MediatR;
using Application.Interfaces;
using Application.Exceptions;
using Domain;
using Microsoft.EntityFrameworkCore;
using Application.Converters;

namespace Application.Requests.Commands.UpdateRequest
{
    public class UpdateRequestCommandHandler : IRequestHandler<UpdateRequestCommand, Request>
    {
        private readonly IDbContext _dbContext;

        public UpdateRequestCommandHandler(IDbContext dbContext) => _dbContext = dbContext;

        public async Task<Request> Handle(UpdateRequestCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Requests
                .Include(req => req.Client)
                .Include(req => req.Manager)
				.Include(req => req.Document)
				.Include(req => req.Files)
                .FirstOrDefaultAsync(req => req.RequestId == request.Id, cancellationToken) ?? 
                throw new NotFoundException(nameof(Request), request.Id.ToString());

            if (request.Date != null)
                entity.Date = request.Date.Value;

            if (!string.IsNullOrEmpty(request.ReasonRequest))
                entity.ReasonRequest = request.ReasonRequest;

            if (!string.IsNullOrEmpty(request.NecessaryFunds))
                entity.NecessaryFunds = request.NecessaryFunds;

            if (request.ManagerId != null)
            {
                var manager = await _dbContext.Managers.FirstOrDefaultAsync(m => m.ManagerId == request.ManagerId, cancellationToken) ?? 
                    throw new NotFoundException(nameof(Request), request.ManagerId.ToString());

                entity.Manager = manager;
            }

            if (!string.IsNullOrEmpty(request.InternalInfo))
                entity.InternalInfo = request.InternalInfo;

            if (request.Status != null)
                entity.Status = request.Status.Value;

            if (request.WorkResultType != null)
                entity.WorkResultType = request.WorkResultType.Value;

            if (!string.IsNullOrEmpty(request.ActFilePath))
                entity.ActFilePath = request.ActFilePath;

            if (request.Document != null)
            {
                var document = new Document()
                {
                    Title = request.Document.Title,
                    Extension = request.Document.Extension,
                    File = request.Document.File,
					Hash = GetSHAHash.ComputeSha256Hash(request.Document.File)
                };

				var dbDoc = await _dbContext.Documents.FirstOrDefaultAsync(d => d.Hash == document.Hash);

				if (dbDoc == null)
				{
					await _dbContext.Documents.AddAsync(document, cancellationToken);
					entity.Document = document;
				}
				else
				{
					entity.Document = dbDoc;
				}
			}

            await _dbContext.SaveChangesAsync(cancellationToken);

			if (entity.Files != null)
			{
				entity.Files = entity.Files.Select(d => new Document()
				{
					DocumentId = d.DocumentId,
					Title = d.Title,
				}).ToList();
			}

			if (entity.Document != null)
			{
				entity.Document = new Document()
				{
					Title = entity.Document.Title,
					DocumentId = entity.Document.DocumentId,
				};
			}

            return entity;
        }
    }
}
