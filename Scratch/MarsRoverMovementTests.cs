using MarsRover;
using MarsRover.Exceptions;
using NUnit.Framework;

namespace Scratch
{
    [TestFixture]
    public class MarsRoverMovementTests
    {
        [Test]
        public void CanSetSizeOfPlataeu()
        {
            var size = 4;
            var mars = CreateSquarePlataeu(size);
            Assert.That(mars.NorthSouthLength, Is.EqualTo(size));
            Assert.That(mars.EastWestLength, Is.EqualTo(size));
        }

        [TestCase("0 0 N")]
        [TestCase("1 1 E")]
        [TestCase("3 1 S")]
        [TestCase("1 3 W")]
        public void CanLandRobotAtSpecificCordinatesAndDirection(string directedPosition)
        {
            var mars = CreateSquarePlataeu(4);
            var robot = new Robot();
            
            robot.Land(mars, new DirectedPosition(directedPosition));

            Assert.That(robot.DirectedPosition(), Is.EqualTo(directedPosition));
        }

        [TestCase("5 6 N")]
        [TestCase("4 5 N")]
        [TestCase("4 4 N")]
        [TestCase("-1 1 N")]
        [TestCase("1 -1 N")]
        public void CannotLandRobotOutSideOfPlataeu(string directedPositon)
        {
            var mars = CreateSquarePlataeu(4);
            var robot = new Robot();

            Assert.Throws<CantLandThereException>(() => robot.Land(mars, new DirectedPosition(directedPositon)));
        }

        [Test]
        public void CannotLandWithAnInvalidHeading()
        {
            var mars = CreateSquarePlataeu(4);
            var robot = new Robot();

            AssertThrowsNotAValidDirectionException("", mars, robot);
            AssertThrowsNotAValidDirectionException("n", mars, robot);
            AssertThrowsNotAValidDirectionException(null, mars, robot);
            AssertThrowsNotAValidDirectionException("11", mars, robot);
            AssertThrowsNotAValidDirectionException("invalid", mars, robot);
            AssertThrowsNotAValidDirectionException("M", mars, robot);
        }

        [TestCase("N", "E")]
        [TestCase("E", "S")]
        [TestCase("S", "W")]
        [TestCase("W", "N")]
        public void WhenRobotLandedCanTurnRight(string startingHeading, string endHeading)
        {
            var mars = CreateSquarePlataeu(4);
            var robot = new Robot();
            
            LandRobotWithHeading(startingHeading, mars, robot);

            robot.Move("R");

            AssertRobotHeading(endHeading, robot);
        }

        [TestCase("N", "W")]
        [TestCase("W", "S")]
        [TestCase("S", "E")]
        [TestCase("E", "N")]
        public void WhenRobotLandedCanTurnLeft(string startingHeading, string endHeading)
        {
            var mars = CreateSquarePlataeu(4);
            var robot = new Robot();

            LandRobotWithHeading(startingHeading, mars, robot);

            robot.Move("L");

            AssertRobotHeading(endHeading, robot);
        }

        [TestCase("N", "1 2")]
        [TestCase("E", "2 1")]
        [TestCase("S", "1 0")]
        [TestCase("W", "0 1")]
        public void WhenRobotLandedRobotCanMoveInDirectionOfHeading(string startHeading, string endPosition)
        {
            var mars = CreateSquarePlataeu(3);
            var robot = new Robot();

            LandRobotWithHeadingInCentreOfPlataeu(startHeading, mars, robot);

            robot.Move("M");

            Assert.That(robot.DirectedPosition(), Is.EqualTo(endPosition + " " + startHeading));
        }

        [TestCase("MM", "0 2 N")]
        [TestCase("MRM", "1 1 E")]
        [TestCase("MRMML", "2 1 N")]
        public void RobotCanHandleTwoMovementCommands(string movement, string endDirectedPosition)
        {
            var mars = CreateSquarePlataeu(3);
            var robot = new Robot();

            robot.Land(mars, new DirectedPosition("0 0 N"));

            robot.Move(movement);

            Assert.That(robot.DirectedPosition(), Is.EqualTo(endDirectedPosition));
        }

        [TestCase("0 0 N", "LM")]
        [TestCase("0 0 N", "LLM")]
        [TestCase("2 2 N", "M")]
        [TestCase("2 2 N", "RM")]
        public void RobotWillNotMoveOutOfPlataeu(string startingDirectedPositon, string movement)
        {
            var mars = CreateSquarePlataeu(3);
            var robot = new Robot();

            robot.Land(mars, new DirectedPosition(startingDirectedPositon));

            Assert.Throws<ImNotGoingThereException>(() => robot.Move(movement));
        }

        private static void LandRobotWithHeadingInCentreOfPlataeu(string startHeading, Plataeu mars, Robot robot)
        {
            robot.Land(mars, new DirectedPosition("1 1 " + startHeading));
        }

        private static void AssertRobotHeading(string endHeading, Robot robot)
        {
            Assert.That(robot.DirectedPosition(), Is.EqualTo("0 0 " + endHeading));
        }

        private static void LandRobotWithHeading(string startingHeading, Plataeu mars, Robot robot)
        {
            robot.Land(mars, new DirectedPosition("0 0 " + startingHeading));
        }

        private static void AssertThrowsNotAValidDirectionException(string direction, Plataeu mars, Robot robot)
        {
            Assert.Throws<NotAValidDirectionException>(() => robot.Land(mars, new DirectedPosition("0 0 " + direction)));
        }

        private Plataeu CreateSquarePlataeu(int size)
        {
            var mars = new Plataeu(size, size);
            return mars;
        }
    }
}
