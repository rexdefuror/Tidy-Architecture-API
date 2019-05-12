using MediatR;
using NetLore.Data.Contexts;
using System.Threading;
using System.Threading.Tasks;

namespace NetLore.Application.Write.TaskLists
{
    public class DeleteTaskListRequestHandler : IRequestHandler<DeleteTaskListRequest>
    {
        private readonly TrackingContext _context;

        public DeleteTaskListRequestHandler(TrackingContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteTaskListRequest request, CancellationToken cancellationToken)
        {
            var entity = await _context.TaskLists.FindAsync(request.Id);
            if (entity == null)
            {
                throw new System.Exception();
            }
            _context.TaskLists.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
