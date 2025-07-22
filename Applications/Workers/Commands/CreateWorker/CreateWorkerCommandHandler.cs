using Application.Interfaces;
using Domain;
using MediatR;

namespace Application.Workers.Commands.CreateWorker
{
    public class CreateWorkerCommandHandler : IRequestHandler<CreateWorkerCommand, Worker>
    {
        private readonly IDbContext _dbContext;

        public CreateWorkerCommandHandler(IDbContext dbContext) => _dbContext = dbContext;

        public async Task<Worker> Handle(CreateWorkerCommand request, CancellationToken cancellationToken)
        {
            var entity = new Worker()
            {
                Name = request.Name,
                Description = request.Description,
                ShiftSalary = request.ShiftSalary,
                HourSalary = request.HourSalary,
				ApiKey = Guid.NewGuid().ToString(),
                Experiencies = new List<Experience>()
            };

            await _dbContext.Workers.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity;
        }
    }
}
