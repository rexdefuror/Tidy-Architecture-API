using MediatR;

namespace NetLore.Application.Write.TaskLists
{
    public class DeleteTaskListRequest : IRequest
    {
        public DeleteTaskListRequest(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
