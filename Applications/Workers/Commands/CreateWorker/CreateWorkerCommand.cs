using Domain;
using MediatR;

namespace Application.Workers.Commands.CreateWorker
{
    public class CreateWorkerCommand : IRequest<Worker>
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int ShiftSalary { get; set; }
        public int HourSalary { get; set; }
    }
}
