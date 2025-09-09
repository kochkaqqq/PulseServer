using MediatR;
using Application.Interfaces;
using Application.Exceptions;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Application.Workers.Commands.UpdateWorker
{
    public class UpdateWorkerCommandHandler : IRequestHandler<UpdateWorkerCommand, Worker>
    {
        private readonly IDbContext _dbContext;

        public UpdateWorkerCommandHandler(IDbContext dbContext) => _dbContext = dbContext;

        public async Task<Worker> Handle(UpdateWorkerCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Workers.FirstOrDefaultAsync(w => w.WorkerId == request.Id, cancellationToken) ?? 
                throw new NotFoundException(nameof(Worker), request.Id.ToString());

			if (!string.IsNullOrEmpty(request.Name))
				entity.Name = request.Name;

			if (!string.IsNullOrEmpty(request.Description))
				entity.Description = request.Description;

			if (request.ShiftSalary != null)
				entity.ShiftSalary = request.ShiftSalary.Value;

			if (request.HourSalary != null)
				entity.HourSalary = request.HourSalary.Value;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity;
        }
    }
}
