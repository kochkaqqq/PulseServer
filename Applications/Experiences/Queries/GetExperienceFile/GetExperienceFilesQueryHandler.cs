using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Exceptions;

namespace Application.Experiences.Queries.GetExperienceFile
{
	public class GetExperienceFilesQueryHandler : IRequestHandler<GetExperienceFilesQuery, List<Document>>
	{
		private readonly IDbContext _dbContext;

		public GetExperienceFilesQueryHandler(IDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<List<Document>> Handle(GetExperienceFilesQuery request, CancellationToken cancellationToken)
		{
			var exp = await _dbContext.Experiences
				.AsNoTrackingWithIdentityResolution()
				.Include(e => e.Request)
				.Include(e => e.Request.Files)
				.FirstOrDefaultAsync(e => e.ExperienceId == request.ExperienceId) ?? throw new NotFoundException(nameof(Experience), request.ExperienceId.ToString());

			if (exp.Request.Files == null)
				return new();

			return exp.Request.Files.Select(d => new Document()
			{
				DocumentId = d.DocumentId,
				Title = d.Title,
			}).ToList();
		}
	}
}
