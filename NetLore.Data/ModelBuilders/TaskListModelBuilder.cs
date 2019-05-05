using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetLore.Domain.Entities;

namespace NetLore.Data.ModelBuilders
{
    public class TaskListModelBuilder : IEntityTypeConfiguration<TaskList>
    {
        public void Configure(EntityTypeBuilder<TaskList> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.Tasks)
                .WithOne(x => x.TaskList)
                .HasForeignKey(x => x.TaskListId);
        }
    }
}
