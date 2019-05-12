using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NetLore.Data.Contexts;
using NetLore.Domain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace NetLore.Application.Read.TaskLists
{
    public class FindTaskListByIdRequestHandler : IRequestHandler<FindTaskListByIdRequest, TaskList>
    {
        private readonly NoTrackingContext _context;
        private readonly IMapper _mapper;

        public FindTaskListByIdRequestHandler(NoTrackingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TaskList> Handle(FindTaskListByIdRequest request, CancellationToken cancellationToken)
        {
            var result = await _context.TaskLists.Include(x => x.Tasks).FirstOrDefaultAsync(x => x.Id == request.Id);
            return _mapper.Map<TaskList>(result);
        }
    }
}
