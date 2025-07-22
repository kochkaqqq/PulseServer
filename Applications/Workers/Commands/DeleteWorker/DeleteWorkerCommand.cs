using MediatR;

namespace Application.Workers.Commands.DeleteWorker
{
    public class DeleteWorkerCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
