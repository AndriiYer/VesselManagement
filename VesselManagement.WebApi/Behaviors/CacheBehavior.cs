using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace VesselManagement.WebApi.Behaviors;

public class CacheBehavior<TRequest, TResponse>(IMemoryCache cache, ILogger<CacheBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var cacheKey = request.GetHashCode().ToString();

        if (cache.TryGetValue(cacheKey, out TResponse response))
        {
            logger.LogInformation($"Returning cached response for {typeof(TRequest).Name}");
            return response;
        }

        logger.LogInformation($"Cache miss for {typeof(TRequest).Name}. Invoking handler.");
        response = await next();

        cache.Set(cacheKey, response, TimeSpan.FromMinutes(5));

        return response;
    }
}