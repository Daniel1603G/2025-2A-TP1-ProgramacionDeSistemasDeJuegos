using UnityEngine;

namespace Gameplay
{
    public interface IPlayerState
    {
        void Enter(PlayerController player);
        void Exit();
        void HandleMove(Vector2 input);
        void HandleJump();
        void HandleCollision(Collision collision);
    }
}