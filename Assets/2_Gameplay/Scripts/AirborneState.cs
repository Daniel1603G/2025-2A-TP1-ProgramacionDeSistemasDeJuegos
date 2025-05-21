using UnityEngine;

namespace Gameplay
{
    public class AirborneState : IPlayerState
    {
        private PlayerController _player;
        private bool _usedDoubleJump;

        public void Enter(PlayerController player)
        {
            _player = player;
            _usedDoubleJump = false;
        }

        public void Exit() { }

        public void HandleMove(Vector2 input)
        {
            var direction = input.ToHorizontalPlane() * _player.airborneSpeedMultiplier;
            _player.Character.SetDirection(direction);
        }

        public void HandleJump()
        {
            if (_usedDoubleJump)
                return;

            _player.StartJump();
            _usedDoubleJump = true;
        }

        public void HandleCollision(Collision collision)
        {
            foreach (var contact in collision.contacts)
            {
                if (Vector3.Angle(contact.normal, Vector3.up) < 5)
                {
                    _player.SetState(new GroundedState());
                    break;
                }
            }
        }
    }
}