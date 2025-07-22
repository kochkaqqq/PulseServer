using Application.Interfaces;
using MediatR;
using Domain;
using Application.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Application.Managers.Commands.DeleteManager
{
    public class DeleteManagerCommandHandler : IRequestHandler<DeleteManagerCommand, Unit>
    {
        private readonly IDbContext _dbContext;

        public DeleteManagerCommandHandler(IDbContext dbContext) => _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteManagerCommand request, CancellationToken cancellationToken)
        {
            var manager = await _dbContext.Managers.FirstOrDefaultAsync(m => m.ManagerId == request.Id, cancellationToken) ?? 
                throw new NotFoundException(nameof(Manager), request.Id.ToString());

			_dbContext.Managers.Remove(manager);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
