namespace NetLore.Domain.Entities
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }

        public int TaskListId { get; set; }
        public virtual TaskList TaskList { get; set; }
    }
}
