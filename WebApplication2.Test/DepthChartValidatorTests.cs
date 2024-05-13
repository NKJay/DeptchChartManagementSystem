using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Models;
using WebApplication2.Validators;

namespace WebApplication2.Test
{
    public class DepthChartValidatorTests
    {
        [Fact]
        public void ValidatePlayer_ThrowsArgumentNullException_WhenPlayerIsNull()
        {
            Player? player = null;

            Assert.Throws<ArgumentNullException>(() => DepthChartOperationValidator.ValidatePlayer(player));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("   ")]
        public void ValidatePlayer_ThrowsArgumentException_WhenPlayerNameIsNullOrWhitespace(string name)
        {
            var player = new Player("12",name);

            var exception = Assert.Throws<ArgumentException>(() => DepthChartOperationValidator.ValidatePlayer(player));
            Assert.Equal("Player name cannot be empty", exception.Message);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("   ")]
        public void ValidatePosition_ThrowsArgumentException_WhenPositionIsNullOrWhitespace(string position)
        {

            var exception = Assert.Throws<ArgumentException>(() => DepthChartOperationValidator.ValidatePosition(position));
            Assert.Equal("Position cannot be empty", exception.Message);
        }


        [Theory]
        [InlineData(-1)]
        public void ValidatePositionDepth_ThrowsArgumentException_WhenPositionDepthIsNegative(int? positionDepth)
        {

            var exception = Assert.Throws<ArgumentException>(() => DepthChartOperationValidator.ValidatePositionDepth(positionDepth));
            Assert.Equal("Position depth cannot be negative", exception.Message);
        }


        [Theory]
        [InlineData(0)]
        [InlineData(null)]
        [InlineData(1)]
        public void ValidatePositionDepth_DoesNotThrowException_WhenPositionDepthIsZeroOrPositiveOrNull(int? positionDepth)
        {

            Assert.Null(Record.Exception(() => DepthChartOperationValidator.ValidatePositionDepth(positionDepth)));
        }
    }
}
