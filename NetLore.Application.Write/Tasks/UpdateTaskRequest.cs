using MediatR;
using NetLore.Domain.Models;

namespace NetLore.Application.Write.Tasks
{
    public class UpdateTaskRequest : IRequest
    {
        public UpdateTaskRequest(int id, Task task)
        {
            Id = id;
            Task = task;
        }

        public int Id { get; set; }
        public Task Task { get; set; }
    }
}