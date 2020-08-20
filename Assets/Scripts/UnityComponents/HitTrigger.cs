using UnityEngine;
using Leopotam.Ecs;

namespace MaltaTanks
{
    public class HitTrigger : MonoBehaviour
    {
        public EcsWorld _world;

        private void OnTriggerEnter(Collider other)
        {
            ref var hitEvent = ref _world.NewEntity().Get<CollisionEvent>();
            hitEvent.hitCollider = other;
            hitEvent.emitter = gameObject;
        }
    }
}