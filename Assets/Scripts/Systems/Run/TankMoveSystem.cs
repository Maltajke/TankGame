using Leopotam.Ecs;
using UnityEngine;

namespace MaltaTanks 
{
    sealed class TankMoveSystem : IEcsRunSystem
    {
        readonly EcsFilter<TransformComponent, PropertyComponent> _tank = null;
        readonly ActionMap _input = null;

        void IEcsRunSystem.Run () 
        {            
            foreach(var i in _tank)
            {
                float h = _input.Player.Move.ReadValue<Vector2>().x;
                float v = _input.Player.Move.ReadValue<Vector2>().y;

                ref var transform = ref _tank.Get1(i).transform;
                ref var rigidbody = ref _tank.Get1(i).rigidbody;

                if (v < 0) h = -h;
                float turn = h * 110 * Time.deltaTime; 

                Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
                rigidbody.MoveRotation(transform.rotation * turnRotation);

                Vector3 movement = transform.forward * v * _tank.Get2(i).speed * Time.deltaTime;  
                rigidbody.MovePosition(transform.position + movement);               
            }
        }
    }
}