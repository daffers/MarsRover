namespace MarsRover
{
    public struct Position
    {
        private readonly int _xPosition;
        private readonly int _yPosition;

        public Position(int xPosition, int yPosition)
        {
            _xPosition = xPosition;
            _yPosition = yPosition;
        }

        public int Y
        {
            get { return _yPosition; }
        }

        public int X
        {
            get { return _xPosition; }
        }

        public Position AdvancedXAxis()
        {
            return new Position(_xPosition + 1, _yPosition);
        }

        public Position AdvancedYAxis()
        {
            return new Position(_xPosition, _yPosition + 1);
        }

        public Position ReverseXAxis()
        {
            return new Position(_xPosition - 1, _yPosition);
        }
        public Position ReverseYAxis()
        {
            return new Position(_xPosition, _yPosition - 1);
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", _xPosition, _yPosition);
        }
    }
}