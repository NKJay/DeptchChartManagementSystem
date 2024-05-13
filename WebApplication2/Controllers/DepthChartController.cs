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

        [HttpPost("addPlayerToDepthChart")]
        public IActionResult AddPlayerToDepthChart(string position, [FromBody] Player player, int? positionDepth = null)
        {
            try
            {
                _depthChartOperation.AddPlayerToDepthChart(position, player, positionDepth);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("removePlayerFromDepthChart")]
        public IActionResult RemovePlayerFromDepthChart(string position, [FromBody] Player player)
        {
                var removedPlayer = _depthChartOperation.RemovePlayerFromDepthChart(position, player);
                return Ok(removedPlayer);
        }

        [HttpPost("getBackups")]
        public IActionResult GetBackups(string position, [FromBody] Player player)
        {
                var backups = _depthChartOperation.GetBackups(position, player);
                return Ok(backups);
        }

        [HttpGet("getFullDepthChart")]
        public IActionResult GetFullDepthChart()
        {
            var fullDepthChart = _depthChartOperation.GetFullDepthChart();
            return Ok(fullDepthChart);
        }
    }
}
