namespace MarsRover
{
    public struct Plataeu
    {
        private readonly int _eastWestLength;
        private readonly int _northSouthLength;

        public Plataeu(int eastWestLength, int northSouthLength)
        {
            _northSouthLength = northSouthLength;
            _eastWestLength = eastWestLength;
        }

        public int NorthSouthLength { get { return _northSouthLength; } }
        public int EastWestLength { get { return _eastWestLength; } }

        public bool IsOnMe(Position position)
        {
            return
                (position.X < _eastWestLength) &&
                (position.X >= 0) &&
                (position.Y < _northSouthLength) &&
                (position.Y >= 0);
        }

    }
}