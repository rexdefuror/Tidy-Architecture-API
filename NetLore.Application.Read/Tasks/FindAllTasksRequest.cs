using MediatR;
using System.Collections.Generic;

namespace NetLore.Application.Read.Tasks
{
    public class FindAllTasksRequest : IRequest<List<Domain.Models.Task>>
    {
    }
}
