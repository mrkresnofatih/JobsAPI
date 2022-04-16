using System.Threading.Tasks;
using JobsApi.AuthedGateway.Attributes;
using JobsApi.AuthedGateway.Constants;
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
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        private readonly AuthService _authService;

        [HttpPost("signup")]
        [TypeFilter(typeof(ExceptionLoggingQueueFilterAttribute))]
        public async Task<ResponsePayload<PlayerGetDto>> Signup(Player player)
        {
            var res = await _authService.Signup(player);
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
        public ResponsePayload<string> GetString([FromHeader(Name = CustomHeaders.SpanIdHeader)] string spanId)
        {
            return ResponseHandler.WrapSuccess(spanId);
        }
    }
}