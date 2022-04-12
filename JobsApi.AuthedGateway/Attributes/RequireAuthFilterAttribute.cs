using System;
using System.Threading.Tasks;
using JobsApi.AuthedGateway.Constants;
using JobsApi.AuthedGateway.Exceptions;
using JobsApi.AuthedGateway.Repositories;
using JobsApi.AuthedGateway.Utils;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JobsApi.AuthedGateway.Attributes
{
    public class RequireAuthFilterAttribute : Attribute, IAsyncActionFilter
    {
        public RequireAuthFilterAttribute(PlayerAccessTokenUtility playerAccessTokenUtility, 
            AccessTokenCache accessTokenCache)
        {
            _accessTokenCache = accessTokenCache;
            _playerAccessTokenUtility = playerAccessTokenUtility;
        }

        private readonly AccessTokenCache _accessTokenCache;
        private readonly PlayerAccessTokenUtility _playerAccessTokenUtility;

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var authHeaderExists = context
                .HttpContext
                .Request
                .Headers
                .ContainsKey(CustomHeaders.AuthHeader);
            if (!authHeaderExists)
            {
                throw new InvalidCredException();
            }

            var token = context.HttpContext.Request.Headers[CustomHeaders.AuthHeader];
            var isTokenValid = _playerAccessTokenUtility.ValidateToken(token);

            if (!isTokenValid)
            {
                throw new InvalidCredException();
            }

            var usernameFromToken = _playerAccessTokenUtility.GetUsernameFromToken(token);
            var tokenFromRedis = await _accessTokenCache.GetByUsername(usernameFromToken);
            if (tokenFromRedis == null)
            {
                throw new InvalidCredException();
            }

            var extendTokenLifeTask = _accessTokenCache.SaveByUsername(usernameFromToken, token);
            var controllerExecutionTask = Task.Run(() => next());
            Task.WaitAll(extendTokenLifeTask, controllerExecutionTask);
        }
    }
}