using AutoMapper;
using MediatR;
using NetLore.Data.Contexts;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NetLore.Application.Write.Tasks
{
    public class UpdateTaskRequestHandler : IRequestHandler<UpdateTaskRequest>
    {
        private readonly TrackingContext _context;
        private readonly IMapper _mapper;

        public UpdateTaskRequestHandler(TrackingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateTaskRequest request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Domain.Entities.Task>(request.Task);

            if (!_context.Tasks.Local.Any(x => x.Id == request.Id))
            {
                _context.Update(entity);
            }

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
