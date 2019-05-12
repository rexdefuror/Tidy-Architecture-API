using AutoMapper;
using MediatR;
using NetLore.Data.Contexts;
using System.Threading;
using System.Threading.Tasks;

namespace NetLore.Application.Write.Tasks
{
    public class SaveTaskRequestHandler : IRequestHandler<SaveTaskRequest>
    {
        private readonly TrackingContext _context;
        private readonly IMapper _mapper;

        public SaveTaskRequestHandler(TrackingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(SaveTaskRequest request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Domain.Entities.Task>(request.Task);
            await _context.Tasks.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}