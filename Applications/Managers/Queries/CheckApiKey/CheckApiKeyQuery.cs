using MediatR;

namespace Application.Managers.Queries.CheckApiKey
{
	public class CheckApiKeyQuery : IRequest<bool>
	{
		public string ApiKey { get; set; } = string.Empty;
	}
}
