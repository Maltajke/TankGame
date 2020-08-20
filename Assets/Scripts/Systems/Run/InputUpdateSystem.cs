using Leopotam.Ecs;
using UnityEngine.InputSystem;

namespace MaltaTanks 
{
    sealed class InputUpdateSystem : IEcsRunSystem 
    {
        void IEcsRunSystem.Run () 
        {
            InputSystem.Update();
        }
    }
}