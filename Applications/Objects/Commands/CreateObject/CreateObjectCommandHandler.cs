using Application.Interfaces;
using MediatR;
using Domain;

namespace Application.Objects.Commands.CreateObject
{
    public class CreateObjectCommandHandler : IRequestHandler<CreateObjectCommand, Domain.Client>
    {
        private readonly IDbContext _dbContext;

        public CreateObjectCommandHandler(IDbContext dbContext) => _dbContext = dbContext;

        public async Task<Client> Handle(CreateObjectCommand request, CancellationToken cancellationToken)
        {
            var entity = new Client()
            {
                Name = request.Name,
                Address = request.Address,
                Contact = request.Contact,
                EMail = request.EMail,
                Phone = request.Phone,
                CreatedDate = DateTime.Now,
            };

            await _dbContext.Clients.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity;
        }
    }
}
