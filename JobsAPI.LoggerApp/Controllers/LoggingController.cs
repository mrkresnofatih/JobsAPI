using Microsoft.AspNetCore.Mvc;
using Serilog.Core;

namespace JobsAPI.LoggerApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoggingController
    {
        public LoggingController(Logger logger)
        {
            _logger = logger;
        }

        private readonly Logger _logger;

        [HttpGet("test")]
        public object Get()
        {
            _logger.Error("test is error, maybe");
            return new {Data = "data", Error = "" };
        }
    }
}