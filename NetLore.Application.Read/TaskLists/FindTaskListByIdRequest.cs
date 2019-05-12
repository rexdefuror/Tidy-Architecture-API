using MediatR;

namespace NetLore.Application.Read.TaskLists
{
    public class FindTaskListByIdRequest : IRequest<Domain.Models.TaskList>
    {
        public FindTaskListByIdRequest(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
