using WebApplication2.Exceptions;
using WebApplication2.Models;
using WebApplication2.Validators;

namespace WebApplication2.Operations
{
    public class DepthChartOperations
    {

        private Dictionary<string, List<Player>> _depthChart;

        public DepthChartOperations()
        {
            _depthChart = new Dictionary<string, List<Player>>();
        }

        public void AddPlayerToDepthChart(string position, Player player, int? positionDepth = null)
        {
            DepthChartOperationValidator.ValidatePlayer(player);
            DepthChartOperationValidator.ValidatePosition(position);
            DepthChartOperationValidator.ValidatePositionDepth(positionDepth);

            if (!_depthChart.ContainsKey(position))
            {
                _depthChart[position] = new List<Player>();
            }

            if (_depthChart[position].Exists((item) => { return player.Number == item.Number; }))
            {
                throw new ConflictException($"Player already exists in the depth chart for position {position}");
            }

            if (positionDepth == null || positionDepth >= _depthChart[position].Count)
            {
                _depthChart[position].Add(player);
            }
            else
            {
                _depthChart[position].Insert(positionDepth.Value, player);
                UpdatePositionDepth(position);
            }
        }

        public List<Player> RemovePlayerFromDepthChart(string position, Player player)
        {
            DepthChartOperationValidator.ValidatePlayer(player);
            DepthChartOperationValidator.ValidatePosition(position);

            if (!_depthChart.ContainsKey(position) || !_depthChart[position].Remove(player))
            {
                return new List<Player> { };
            }

            UpdatePositionDepth(position);
            return new List<Player> { player  };
        }

        public List<Player> GetBackups(string position, Player player)
        {
            DepthChartOperationValidator.ValidatePlayer(player);
            DepthChartOperationValidator.ValidatePosition(position);

            if (!_depthChart.ContainsKey(position))
            {
                return new List<Player>();
            }

            int index = _depthChart[position].FindIndex(p => p.Equals(player));
            if (index == -1)
            {
                return new List<Player>();
            }

            if (index == _depthChart[position].Count - 1)
            {
                return new List<Player>(); // No backups if the player is the last in the depth chart
            }

            return _depthChart[position].GetRange(index + 1, _depthChart[position].Count - index - 1);
        }

        public Dictionary<string, List<Player>> GetFullDepthChart()
        {
            foreach (var position in _depthChart)
            {
                Console.WriteLine($"Position: {position.Key}");
                foreach (var player in position.Value)
                {
                    Console.WriteLine($"  Player: {player.Name}"); // 假设Player类有一个Name属性
                }
            }
            return _depthChart;
        }

        private void UpdatePositionDepth(string position)
        {
            for (int i = 0; i < _depthChart[position].Count; i++)
            {
                _depthChart[position][i].PositionDepth = i + 1;
            }
        }
    }
}
