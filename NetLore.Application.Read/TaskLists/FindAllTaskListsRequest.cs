using MediatR;
using NetLore.Domain.Models;
using System.Collections.Generic;

namespace NetLore.Application.Read.TaskLists
{
    public class FindAllTaskListsRequest : IRequest<List<TaskList>>
    {
    }
}
