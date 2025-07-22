using Application.Interfaces;
using MediatR;
using Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using Domain;

namespace Application.Objects.Commands.DeleteObject
{
    public class DeleteObjectCommandHandler : IRequestHandler<DeleteObjectCommand, Unit>
    {
        private readonly IDbContext _dbContext;

        public DeleteObjectCommandHandler(IDbContext dbContext) => _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteObjectCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Clients.FirstOrDefaultAsync(c => c.ClientId == request.ClientId) ?? 
                throw new NotFoundException(nameof(Client), request.ClientId.ToString());

            _dbContext.Clients.Remove(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
