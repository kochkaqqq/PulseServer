using MediatR;

namespace Application.ApiKeys.CheckKey
{
	public class CheckApiKeyQuery : IRequest<bool>
	{
		public string ApiKey { get; set; } = null!;
	}
}
