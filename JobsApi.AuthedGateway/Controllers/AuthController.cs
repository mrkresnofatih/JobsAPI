using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JobsApi.AuthedGateway.Attributes;
using JobsApi.AuthedGateway.Controllers.Interfaces;
using JobsApi.AuthedGateway.Models;
using JobsApi.AuthedGateway.Services;
using JobsApi.AuthedGateway.Utils;
using Microsoft.AspNetCore.Mvc;

namespace JobsApi.AuthedGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : IAuthController
    {
        public AuthController(AuthService authService, LoggingQueueUtility loggingQueueUtility)
        {
            _authService = authService;
            _loggingQueueUtility = loggingQueueUtility;
        }

        private readonly LoggingQueueUtility _loggingQueueUtility;
        private readonly AuthService _authService;

        [HttpPost("signup")]
        [TypeFilter(typeof(ExceptionLoggingQueueFilterAttribute))]
        public async Task<ResponsePayload<PlayerGetDto>> Signup(Player player)
        {
            var tasks = new List<Task>
            {
                _loggingQueueUtility
                    .QueueInfoLog("authedGateway",
                        DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString(),
                        "signup request hit!"),
                _authService.Signup(player)
            };
            await Task.WhenAll(tasks.ToArray());
            var res = ((Task<PlayerGetDto>)tasks[1]).Result;
            return ResponseHandler.WrapSuccess(res);
        }

        [HttpPost("login")]
        [TypeFilter(typeof(ExceptionLoggingQueueFilterAttribute))]
        public async Task<ResponsePayload<PlayerLoginResponseDto>> Login(PlayerLoginRequestDto playerLoginRequestDto)
        {
            var res = await _authService.Login(playerLoginRequestDto);
            return ResponseHandler.WrapSuccess(res);
        }

        [HttpGet("test")]
        [TypeFilter(typeof(RequireAuthFilterAttribute))]
        [TypeFilter(typeof(ExceptionLoggingQueueFilterAttribute))]
        public ResponsePayload<string> GetString()
        {
            return ResponseHandler.WrapSuccess("data");
        }
    }
}