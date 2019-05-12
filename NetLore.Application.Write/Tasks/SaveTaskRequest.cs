using MediatR;
using NetLore.Domain.Models;

namespace NetLore.Application.Write.Tasks
{
    public class SaveTaskRequest : IRequest
    {
        public SaveTaskRequest(Task task)
        {
            Task = task;
        }

        public Task Task { get; set; }
    }
}
