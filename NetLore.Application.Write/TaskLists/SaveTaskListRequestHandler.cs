using AutoMapper;
using MediatR;
using NetLore.Data.Contexts;
using System.Threading;
using System.Threading.Tasks;

namespace NetLore.Application.Write.TaskLists
{
    public class SaveTaskListRequestHandler : IRequestHandler<SaveTaskListRequest>
    {
        private readonly TrackingContext _context;
        private readonly IMapper _mapper;

        public SaveTaskListRequestHandler(TrackingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(SaveTaskListRequest request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Domain.Entities.TaskList>(request.TaskList);
            await _context.TaskLists.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
