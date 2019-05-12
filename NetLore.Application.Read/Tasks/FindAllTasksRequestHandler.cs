using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NetLore.Data.Contexts;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NetLore.Application.Read.Tasks
{
    public class FindAllTasksRequestHandler : IRequestHandler<FindAllTasksRequest, List<Domain.Models.Task>>
    {
        private readonly NoTrackingContext _context;
        private readonly IMapper _mapper;

        public FindAllTasksRequestHandler(NoTrackingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Domain.Models.Task>> Handle(FindAllTasksRequest request, CancellationToken cancellationToken)
        {
            var result = await _context.Tasks.ToListAsync(cancellationToken);
            return _mapper.Map<List<Domain.Models.Task>>(result);
        }
    }
}
