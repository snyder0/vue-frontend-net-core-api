using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace StarterApi.Infrastructure.Mediatr
{
    public class LoggingBehavior<TRequest, TResponse>
          : IPipelineBehavior<TRequest, TResponse>
          where TRequest : IRequest<TResponse>
    {
        private readonly IRequestHandler<TRequest, TResponse> _inner;
        private readonly ILogger _logger;

        public LoggingBehavior(
            IRequestHandler<TRequest, TResponse> inner,
            ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _inner = inner;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _logger.Log(LogLevel.Information, $"Handling {typeof(TRequest).Name}");
            var response = await next();
            _logger.Log(LogLevel.Information, $"Handled {typeof(TRequest).Name}");
            return response;
        }
    }
}
