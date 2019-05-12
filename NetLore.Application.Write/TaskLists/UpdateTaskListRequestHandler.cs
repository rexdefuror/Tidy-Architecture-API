using AutoMapper;
using MediatR;
using NetLore.Data.Contexts;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NetLore.Application.Write.TaskLists
{
    public class UpdateTaskListRequestHandler : IRequestHandler<UpdateTaskListRequest>
    {
        private readonly TrackingContext _context;
        private readonly IMapper _mapper;

        public UpdateTaskListRequestHandler(TrackingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateTaskListRequest request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Domain.Entities.TaskList>(request.TaskList);

            if (!_context.TaskLists.Local.Any(x => x.Id == request.Id))
            {
                _context.Update(entity);
            }

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
