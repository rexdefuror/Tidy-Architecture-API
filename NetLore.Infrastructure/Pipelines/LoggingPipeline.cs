using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace NetLore.Infrastructure.Pipelines
{
    public class LoggingPipeline<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IRequestHandler<TRequest, TResponse> _inner;
        private readonly ILogger _logger;

        public LoggingPipeline(IRequestHandler<TRequest, TResponse> inner, ILogger<LoggingPipeline<TRequest, TResponse>> logger)
        {
            _inner = inner;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var name = typeof(TRequest).Name;

            // todo: add more request details
            string json;
            try
            {
                json = JsonConvert.SerializeObject(request, Formatting.Indented);
            }
            catch
            {
                json = string.Empty;
            }

            using (_logger.BeginScope(("request_detail", json)))
            {
                _logger.LogDebug($"Request {name}");
            }

            return await _inner.Handle(request, cancellationToken);
        }
    }
}
