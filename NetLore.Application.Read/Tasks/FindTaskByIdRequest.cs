using MediatR;

namespace NetLore.Application.Read.Tasks
{
    public class FindTaskByIdRequest : IRequest<Domain.Models.Task>
    {
        public FindTaskByIdRequest(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
