using Domain;
using MediatR;

namespace Application.Workers.Queries.GetWorkerDetails
{
    public class GetWorkerDetailsQuery : IRequest<Worker>
    {
        public int Id { get; set; }
    }
}
