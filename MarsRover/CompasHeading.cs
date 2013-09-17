using System.Collections.Generic;
using MarsRover.Exceptions;

namespace MarsRover
{
    public struct CompasHeading
    {
        private static readonly List<string> Headings;
        private readonly int _currentHeadingIndex;
        public string Direction { get { return Headings[_currentHeadingIndex]; } }

        static CompasHeading()
        {
            Headings = new List<string> { "N", "E", "S", "W" };
        }

        public CompasHeading(string direction)
        {
            if (!IsValidHeading(direction))
                throw new NotAValidDirectionException();

            _currentHeadingIndex = Headings.IndexOf(direction);
        }
        
        private CompasHeading(int index)
        {
            _currentHeadingIndex = index;
        }

        public CompasHeading TurnLeft()
        {
            return  (_currentHeadingIndex == 0) ? new CompasHeading(3) : new CompasHeading(_currentHeadingIndex - 1);
        }

        public CompasHeading TurnRight()
        {
            return _currentHeadingIndex == 3 ?  new CompasHeading(0) : new CompasHeading(_currentHeadingIndex + 1);
        }

        public static bool IsValidHeading(string heading)
        {
            return Headings.Contains(heading);
        }
    }
}