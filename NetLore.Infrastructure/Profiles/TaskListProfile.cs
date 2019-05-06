using AutoMapper;

namespace NetLore.Infrastructure.Profiles
{
    public class TaskListProfile : Profile
    {
        public TaskListProfile()
        {
            CreateMap<Domain.Entities.TaskList, Domain.Models.TaskList>()
                .ForMember(target => target.Id, source => source.MapFrom(prop => prop.Id))
                .ForMember(target => target.Name, source => source.MapFrom(prop => prop.Name))
                .ForMember(target => target.Description, source => source.MapFrom(prop => prop.Description))
                .ForMember(target => target.Tasks, source => source.MapFrom(prop => prop.Tasks));

            CreateMap<Domain.Models.TaskList, Domain.Entities.TaskList>()
                .ForMember(target => target.Id, source => source.MapFrom(prop => prop.Id))
                .ForMember(target => target.Name, source => source.MapFrom(prop => prop.Name))
                .ForMember(target => target.Description, source => source.MapFrom(prop => prop.Description))
                .ForMember(target => target.Tasks, source => source.MapFrom(prop => prop.Tasks));
        }
    }
}
