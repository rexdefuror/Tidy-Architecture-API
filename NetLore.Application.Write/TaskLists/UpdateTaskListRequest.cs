using MediatR;
using NetLore.Domain.Models;

namespace NetLore.Application.Write.TaskLists
{
    public class UpdateTaskListRequest : IRequest
    {
        public UpdateTaskListRequest(int id, TaskList taskList)
        {
            Id = id;
            TaskList = taskList;
        }

        public int Id { get; set; }
        public TaskList TaskList { get; set; }
    }
}
