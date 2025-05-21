using UnityEngine;

namespace Gameplay
{
    public class GroundedState : IPlayerState
    {
        private PlayerController _player;

        public void Enter(PlayerController player) => _player = player;
        public void Exit() { }

        public void HandleMove(Vector2 input)
        {
            var direction = input.ToHorizontalPlane();
            _player.Character.SetDirection(direction);
        }

        public void HandleJump()
        {
            _player.StartJump();
            _player.SetState(new AirborneState());
        }

        public void HandleCollision(Collision collision) { }
    }
}