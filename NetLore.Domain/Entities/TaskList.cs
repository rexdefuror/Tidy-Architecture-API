using System.Collections.Generic;

namespace NetLore.Domain.Entities
{
    public class TaskList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual IEnumerable<Task> Tasks { get; set; }
    }
}
