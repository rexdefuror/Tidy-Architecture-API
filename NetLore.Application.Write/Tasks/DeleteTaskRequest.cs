using MediatR;

namespace NetLore.Application.Write.Tasks
{
    public class DeleteTaskRequest : IRequest
    {
        public DeleteTaskRequest(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
