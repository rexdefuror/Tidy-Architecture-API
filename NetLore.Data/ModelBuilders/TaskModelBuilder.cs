using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetLore.Domain.Entities;

namespace NetLore.Data.ModelBuilders
{
    public class TaskModelBuilder : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
