using Domain;
using MediatR;

namespace Application.Workers.Commands.UpdateWorker
{
    public class UpdateWorkerCommand : IRequest<Worker>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int ShiftSalary { get; set; }
        public int HourSalary { get; set; }
    }
}
