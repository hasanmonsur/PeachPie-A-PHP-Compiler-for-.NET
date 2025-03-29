using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace PeachPieWebApp.Server.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PhpApiController : ControllerBase
    {
        private readonly PhpExecutor _php;

        public PhpApiController(PhpExecutor php)
        {
            _php = php;
        }

        [HttpGet("phpinfo")]
        public IActionResult phpinfo()
        {
            try
            {
                var result = _php.CallPhpFunction("getServerInfo", "");

                return Ok(result.ToString());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("greet")]
        public IActionResult Greet([FromQuery] string name = "World")
        {
            try
            {
                var result = _php.CallPhpFunction("greet", name);

                return Ok(result.ToString());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("calculate")]
        public IActionResult Calculate([FromBody] CalculationRequest request)
        {
            try
            {
                var result = _php.CallPhpFunction(
                    $"Calculator::{request.Operation}",
                    request.X,
                    request.Y
                );
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    public record CalculationRequest(string Operation, int X, int Y);
}
