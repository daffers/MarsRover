using System;
using System.Text.RegularExpressions;
using MarsRover.Exceptions;

namespace MarsRover
{
    public struct DirectedPosition
    {
        private readonly CompasHeading _direction;
        private readonly Position _position;

        public DirectedPosition(string directedPosition)
        {
            var regex = new Regex(@"(?<x>-?\d+) (?<y>-?\d+) (?<direction>[NESW])");
            var matches = regex.Match(directedPosition);
            if (!matches.Success)
                throw new NotAValidDirectionException();

            var xPosition = int.Parse(matches.Groups["x"].ToString());
            var yPosition = int.Parse(matches.Groups["y"].ToString());
            _position = new Position(xPosition, yPosition);
            _direction = new CompasHeading(matches.Groups["direction"].ToString());
        }

        private DirectedPosition(Position position, CompasHeading direction)
        {
            _position = position;
            _direction = direction;
        }

        public Position Position { get { return _position; } }
        
        public DirectedPosition ApplyMovement(string movement)
        {
            if (movement == "M")
                return MoveInDirectionOfHeading();
            if (movement == "R")
                return new DirectedPosition(_position, _direction.TurnRight());
            if (movement == "L")
                return new DirectedPosition(_position, _direction.TurnLeft());
            
            throw new NotAValidDirectionException();
        }

        private DirectedPosition MoveInDirectionOfHeading()
        {
            if (_direction.Direction == "N")
                return new DirectedPosition(_position.AdvancedYAxis(), _direction);
            if (_direction.Direction == "E")
                return new DirectedPosition(_position.AdvancedXAxis(), _direction);
            if (_direction.Direction == "S")
                return new DirectedPosition(_position.ReverseYAxis(), _direction);
            if (_direction.Direction == "W")
                return new DirectedPosition(_position.ReverseXAxis(), _direction);
           
            throw new ApplicationException();
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", _position, _direction.Direction);
        }
    }
}