using Microsoft.Extensions.Caching.Distributed;

namespace DigitalTribe
{
    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ITokenValidationService _tokenValidationService;
        private readonly IDistributedCache _cache;

        public TokenValidationMiddleware(RequestDelegate next, ITokenValidationService tokenValidationService, IDistributedCache cache)
        {
            _next = next;
            _tokenValidationService = tokenValidationService;
            _cache = cache;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (IsValidToken(token))
            {
                await _next(context);
            }
            else
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized");
            }
        }

        private bool IsValidToken(string token)
        {
            // Check the cache for the token
            var cacheKey = "Token_" + token;
            var cachedToken = _cache.GetString(cacheKey);

            if (cachedToken != null)
            {
                return true; // Token found in the cache, consider it valid
            }

            // Token not found in the cache, perform validation against the database
            var isValid = _tokenValidationService.ValidateToken(token);

            if (isValid)
            {
                // Cache the token with a sliding expiration
                var cacheOptions = new DistributedCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromMinutes(15) // Adjust as needed
                };
                _cache.SetString(cacheKey, token, cacheOptions);
            }

            return isValid;
        }
    }
}
