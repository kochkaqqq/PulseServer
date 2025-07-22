using Application.Interfaces;
using Domain;
using MediatR;

namespace Application.Managers.Commands.CreateManager
{
    public class CreateManagerCommandHandler : IRequestHandler<CreateManagerCommand, Manager>
    {
        private readonly IDbContext _dbContext;

        public CreateManagerCommandHandler(IDbContext dbContext) => _dbContext = dbContext;

        public async Task<Manager> Handle(CreateManagerCommand request, CancellationToken cancellationToken)
        {
            var manager = new Manager()
            {
                Name = request.Name,
                Description = request.Description,
                ApiKey = Guid.NewGuid().ToString()
            };

            await _dbContext.Managers.AddAsync(manager, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return manager;
        }
    }
}
