using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Exceptions;
using Domain;

namespace Application.Managers.Commands.UpdateManager
{
    public class UpdateManagerCommandHandler : IRequestHandler<UpdateManagerCommand, Manager>
    {
        private readonly IDbContext _dbContext;

        public UpdateManagerCommandHandler(IDbContext dbContext) => _dbContext = dbContext;

        public async Task<Manager> Handle(UpdateManagerCommand request, CancellationToken cancellationToken)
        {
            var manager = await _dbContext.Managers.FirstOrDefaultAsync(entity => entity.ManagerId == request.Id, cancellationToken) ?? 
                throw new NotFoundException(nameof(Manager), request.Id.ToString());

            if (request.Name != null)
			    manager.Name = request.Name;

            if (manager.Description != null)
                manager.Description = request.Description;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return manager;
        }
    }
}
