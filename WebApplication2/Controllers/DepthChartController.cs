using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.Operations;

namespace WebApplication2.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepthChartController : ControllerBase
    {
        private readonly DepthChartOperations _depthChartOperation;

        public DepthChartController()
        {
            _depthChartOperation = new DepthChartOperations();
        }

        [HttpPost("/player")]
        public IActionResult AddPlayerToDepthChart(string position, [FromBody] Player player, int? positionDepth = null)
        {
            _depthChartOperation.AddPlayerToDepthChart(position, player, positionDepth);
            return Ok();
        }

        [HttpDelete("/player")]
        public IActionResult RemovePlayerFromDepthChart(string position, [FromBody] Player player)
        {
            var removedPlayer = _depthChartOperation.RemovePlayerFromDepthChart(position, player);
            return Ok(removedPlayer);
        }

        [HttpPost("/{position}/backups")]
        public IActionResult GetBackups(string position, [FromBody] Player player)
        {
            var backups = _depthChartOperation.GetBackups(position, player);
            return Ok(backups);
        }

        [HttpGet("/fullDepthChart")]
        public IActionResult GetFullDepthChart()
        {
            var fullDepthChart = _depthChartOperation.GetFullDepthChart();
            return Ok(fullDepthChart);
        }
    }
}
