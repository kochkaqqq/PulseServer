using Application.Interfaces;
using MediatR;
using Application.Exceptions;
using Domain;

namespace Application.Requests.Commands.DeleteRequest
{
    public class DeleteRequestCommandHandler : IRequestHandler<DeleteRequestCommand, Unit>
    {
        private readonly IDbContext _dbContext;

		public DeleteRequestCommandHandler(IDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<Unit> Handle(DeleteRequestCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Requests.FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Request), request.Id.ToString());
            }

            _dbContext.Requests.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
