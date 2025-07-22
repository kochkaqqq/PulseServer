using MediatR;

namespace Application.Objects.Commands.CreateObject
{
    public class CreateObjectCommand : IRequest<Domain.Client>
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
		public string Contact { get; set; } = string.Empty;
		public string EMail { get; set; } = string.Empty;
		public string Phone { get; set; } = string.Empty;
	}
}
