using MarsRover.Exceptions;

namespace MarsRover
{
    public class Robot
    {
        private Plataeu _plataeu;
        private DirectedPosition _directedPosition;

        public void Land(Plataeu plataeu, DirectedPosition directedpostion)
        {
            if (!plataeu.IsOnMe(directedpostion.Position))
                throw new CantLandThereException();
            
            _directedPosition = directedpostion;
            _plataeu = plataeu;
        }

        public void Move(string movementSequence)
        {
            var movements = movementSequence.ToCharArray();

            foreach (var movement in movements)
            {
                var movementStep = movement.ToString();
                _directedPosition = _directedPosition.ApplyMovement(movementStep);

                if (!_plataeu.IsOnMe(_directedPosition.Position))
                    throw new ImNotGoingThereException();
            }
        }

        public string DirectedPosition()
        {
            return _directedPosition.ToString();
        }
    }
}