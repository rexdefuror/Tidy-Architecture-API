using MediatR;
using NetLore.Data.Contexts;
using System.Threading;
using System.Threading.Tasks;

namespace NetLore.Application.Write.Tasks
{
    public class DeleteTaskRequestHandler : IRequestHandler<DeleteTaskRequest>
    {
        private readonly TrackingContext _context;

        public DeleteTaskRequestHandler(TrackingContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteTaskRequest request, CancellationToken cancellationToken)
        {
            var entity = await _context.Tasks.FindAsync(request.Id);
            if (entity == null)
            {
                throw new System.Exception();
            }
            _context.Tasks.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
