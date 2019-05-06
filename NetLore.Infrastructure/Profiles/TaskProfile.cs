using AutoMapper;

namespace NetLore.Infrastructure.Profiles
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<Domain.Entities.Task, Domain.Models.Task>()
                .ForMember(target => target.Id, source => source.MapFrom(prop => prop.Id))
                .ForMember(target => target.Name, source => source.MapFrom(prop => prop.Name))
                .ForMember(target => target.Description, source => source.MapFrom(prop => prop.Description))
                .ForMember(target => target.IsCompleted, source => source.MapFrom(prop => prop.IsCompleted));

            CreateMap<Domain.Models.Task, Domain.Entities.Task>()
                .ForMember(target => target.Id, source => source.MapFrom(prop => prop.Id))
                .ForMember(target => target.Name, source => source.MapFrom(prop => prop.Name))
                .ForMember(target => target.Description, source => source.MapFrom(prop => prop.Description))
                .ForMember(target => target.IsCompleted, source => source.MapFrom(prop => prop.IsCompleted));
        }
    }
}
