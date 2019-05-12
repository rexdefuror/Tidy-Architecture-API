using MediatR;
using NetLore.Domain.Models;

namespace NetLore.Application.Write.TaskLists
{
    public class SaveTaskListRequest : IRequest
    {
        public SaveTaskListRequest(TaskList taskList)
        {
            TaskList = taskList;
        }

        public TaskList TaskList { get; set; }
    }
}
