using WebApplication2.Exceptions;
using WebApplication2.Models;
using WebApplication2.Operations;

namespace WebApplication2.Test
{
    public class DepthChartOperationTests
    {
        private readonly DepthChartOperations _operations;

        public DepthChartOperationTests()
        {
            _operations = new DepthChartOperations();
        }

        [Fact]
        public void AddPlayerToDepthChart()
        {
            // 1. Add player to empty list
            var player1 = new Player("12", "Tom Brady");

            _operations.AddPlayerToDepthChart("QB", player1);
            var playersAtQB1 = _operations.GetFullDepthChart()["QB"];
            var targetList1 = new List<Player> { player1 };
            Assert.Equal(playersAtQB1, targetList1);

            // 2. Add player without position depth
            var player2 = new Player("11", "Blaine Gabbert");
            _operations.AddPlayerToDepthChart("QB", player2);
            var playersAtQB2= _operations.GetFullDepthChart()["QB"];
            var targetList2 = new List<Player> { player1, player2 };
            Assert.Equal(playersAtQB2, targetList2);

            // 3. Add player with position depth
            var player3 = new Player("72", "Josh Wells");
            _operations.AddPlayerToDepthChart("QB", player3,0);
            var playersAtQB3 = _operations.GetFullDepthChart()["QB"];
            var targetList3 = new List<Player> { player3, player1, player2 };
            Assert.Equal(playersAtQB3, targetList3);
        }

        [Fact]
        public void AddPlayerToDepthChart_ThrowExceptions()
        {
            // Add the same player twice
            var player = new Player ("12","Tom Brady");
            _operations.AddPlayerToDepthChart("QB", player);
            try
            {
                _operations.AddPlayerToDepthChart("QB", player);
            }
            catch(Exception ex)
            {
                Assert.IsType<ConflictException>(ex);
            }

            // Add player with the same number
            var fakePlayer = new Player("12", "Tom Brondy");
            try
            {
                _operations.AddPlayerToDepthChart("QB", fakePlayer);
            }
            catch (Exception ex)
            {
                Assert.IsType<ConflictException>(ex);
            }
        }

        [Fact]
        public void RemovePlayerFromDepthChart()
        {
            // 1.Remove player from nonexsits position
            var player = new Player("12", "Tom Brady");
            var removedPlayerList1 = _operations.RemovePlayerFromDepthChart("QB", player);
            Assert.Equal(removedPlayerList1, new List<Player> { });

            // 2.Remove nonexsits player from exsits position
            var player2 = new Player("11", "Tom Brondy");
            _operations.AddPlayerToDepthChart("QB", player2);
            var removedPlayerList2 = _operations.RemovePlayerFromDepthChart("QB", player);
            Assert.Equal(removedPlayerList2, new List<Player> { });


            // 3. Remove exsits player from exsits position
            _operations.AddPlayerToDepthChart("QB", player);
            var removedPlayerList3 = _operations.RemovePlayerFromDepthChart("QB", player);
            Assert.Equal(removedPlayerList3.First(), player);

            var playersAtQB = _operations.GetFullDepthChart()["QB"];
            Assert.DoesNotContain(player, playersAtQB);

        }

        [Fact]
        public void GetBackups()
        {
            // 1. Get player's backup from nonexists position
            var player = new Player("12", "Tom Brady");
            var backupsReturnValue1 = _operations.GetBackups("TE", player);
            Assert.Equal(backupsReturnValue1, new List<Player> { });

            // 2. Get nonexists player's backup from nonexists position
            _operations.AddPlayerToDepthChart("QB", player);
            var backupsReturnValue2 = _operations.GetBackups("TE", player);
            Assert.Equal(backupsReturnValue2, new List<Player> { });
            _operations.RemovePlayerFromDepthChart("QB",player);

            // 3. Get player's backup from noexists position
            var player2 =  new Player ("11", "Backup 1") ;
            _operations.AddPlayerToDepthChart("QB", player);
            _operations.AddPlayerToDepthChart("QB", player2);
            var backupsReturnValue3 = _operations.GetBackups("QB", player);
            Assert.Equal(backupsReturnValue3, new List<Player> { player2});
        }

        [Fact]
        public void GetFullDepthChar()
        {
            var player1 = new Player("12", "Tom Brady");
            _operations.AddPlayerToDepthChart("QB",player1);

            var fullDepthChart = new Dictionary<string, List<Player>>
            {
                { "QB", new List<Player> { player1  } }
            };


            var result = _operations.GetFullDepthChart();
            Assert.Equal(result, fullDepthChart);
        }
    }
    }