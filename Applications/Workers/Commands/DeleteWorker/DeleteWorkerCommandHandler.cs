using Application.Interfaces;
using MediatR;
using Application.Exceptions;
using Domain;

namespace Application.Workers.Commands.DeleteWorker
{
    public class DeleteWorkerCommandHandler : IRequestHandler<DeleteWorkerCommand, Unit>
    {
        private readonly IDbContext _dbContext;

		public DeleteWorkerCommandHandler(IDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<Unit> Handle(DeleteWorkerCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Workers.FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Worker), request.Id.ToString());
            }

            _dbContext.Workers.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
