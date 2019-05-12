using AutoMapper;
using MediatR;
using NetLore.Data.Contexts;
using System.Threading;
using System.Threading.Tasks;

namespace NetLore.Application.Read.Tasks
{
    public class FindTaskByIdRequestHandler : IRequestHandler<FindTaskByIdRequest, Domain.Models.Task>
    {
        private readonly NoTrackingContext _context;
        private readonly IMapper _mapper;

        public FindTaskByIdRequestHandler(NoTrackingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Domain.Models.Task> Handle(FindTaskByIdRequest request, CancellationToken cancellationToken)
        {
            var result = await _context.Tasks.FindAsync(request.Id);
            return _mapper.Map<Domain.Models.Task>(result);
        }
    }
}
