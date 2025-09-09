using Application.Exceptions;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.MediaFiles.Commands.UnpinMediaFile
{
	public class UnpinMediaFileCommandHandler : IRequestHandler<UnpinMediaFileCommand>
	{
		private readonly IDbContext _dbContext;

		public UnpinMediaFileCommandHandler(IDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task Handle(UnpinMediaFileCommand request, CancellationToken cancellationToken)
		{
			var mediaFile = await _dbContext.MediaFiles.FirstOrDefaultAsync(m => m.Name == request.FileName, cancellationToken) 
				?? throw new NotFoundException(nameof(MediaFile), request.FileName);

			_dbContext.MediaFiles.Remove(mediaFile);

			await _dbContext.SaveChangesAsync(cancellationToken);
		}
	}
}
