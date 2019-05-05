using Microsoft.EntityFrameworkCore;
using NetLore.Domain.Entities;
using System.Reflection;

namespace NetLore.Data.Contexts
{
    public class TrackingContext : DbContext
    {
        public TrackingContext(DbContextOptions<TrackingContext> options) : base(options)
        {
        }

        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<TaskList> TaskLists { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(TrackingContext)));
        }
    }
}
