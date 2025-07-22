using Domain;
using Domain.enums;
using MediatR;

namespace Application.Requests.Commands.UpdateRequest
{
    public class UpdateRequestCommand : IRequest<Request>
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string? ReasonRequest { get; set; } = null;
        public string? NecessaryFunds { get; set; } = null;
        public int? ManagerId { get; set; } = null;
        public string? InternalInfo { get; set; } = null;
        public Status? Status { get; set; }
        public string? ActFilePath { get; set; } = null;
        public DoneWorkActType? WorkResultType { get; set; } = null;
        public Document? Document { get; set; } = null;
	}
}
