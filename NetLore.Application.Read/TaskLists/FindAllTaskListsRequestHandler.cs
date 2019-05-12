using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NetLore.Data.Contexts;
using NetLore.Domain.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NetLore.Application.Read.TaskLists
{
    public class FindAllTaskListsRequestHandler : IRequestHandler<FindAllTaskListsRequest, List<TaskList>>
    {
        private readonly NoTrackingContext _context;
        private readonly IMapper _mapper;

        public FindAllTaskListsRequestHandler(NoTrackingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TaskList>> Handle(FindAllTaskListsRequest request, CancellationToken cancellationToken)
        {
            var result = await _context.TaskLists.ToListAsync(cancellationToken);
            return _mapper.Map<List<TaskList>>(result);
        }
    }
}
