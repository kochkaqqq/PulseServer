using Application.Interfaces;
using MediatR;
using Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using Domain;

namespace Application.Objects.Commands.UpdateObject
{
    public class UpdateObjectCommandHandler : IRequestHandler<UpdateObjectCommand, Client>
    {
        private readonly IDbContext _dbContext;

        public UpdateObjectCommandHandler(IDbContext dbContext) => _dbContext = dbContext;

        public async Task<Client> Handle(UpdateObjectCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Clients.FirstOrDefaultAsync(obj => obj.ClientId == request.ClientId, cancellationToken) ?? 
                throw new NotFoundException(nameof(Client), request.ClientId.ToString());

            if (request.Name != null && request.Name != string.Empty)
                entity.Name = request.Name;

			if (request.Address != null && request.Address != string.Empty)
				entity.Address = request.Address;

			if (request.Contact != null && request.Contact != string.Empty)
				entity.Contact = request.Contact;

			if (request.Phone != null && request.Phone != string.Empty)
				entity.Phone = request.Phone;

			if (request.Email != null && request.Email != string.Empty)
				entity.EMail = request.Email;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity;
        }
    }
}
