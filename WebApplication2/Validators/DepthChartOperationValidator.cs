using WebApplication2.Models;

namespace WebApplication2.Validators
{
    public class DepthChartOperationValidator
    {
        public static void ValidatePlayer(Player? player)
        {
            if (player == null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            if (string.IsNullOrWhiteSpace(player.Name))
            {
                throw new ArgumentException("Player name cannot be empty");
            }
        }

        public  static void ValidatePosition(string position)
        {
            if (string.IsNullOrWhiteSpace(position))
            {
                throw new ArgumentException("Position cannot be empty");
            }
        }

        public static void ValidatePositionDepth(int? positionDepth)
        {
            if (positionDepth != null && positionDepth < 0)
            {
                throw new ArgumentException("Position depth cannot be negative");
            }
        }
    }
}
