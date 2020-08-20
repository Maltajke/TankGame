using Leopotam.Ecs;
using UnityEngine;

namespace MaltaTanks
{
    sealed class TankTransformInitSystem : IEcsInitSystem
    {
        // auto-injected fields.
        readonly EcsWorld _world = null;
        readonly SceneInfo _sceneInfo = null;
        
        public void Init () 
        {
            var tank = _world.NewEntity();
            tank.Get<TankComponent>();
            ref var tankTransform = ref tank.Get<TransformComponent>();
            tankTransform.@object = _sceneInfo._tank.prefab;
            tankTransform.transform = tankTransform.@object.GetComponent<Transform>();
            tankTransform.rigidbody = tankTransform.@object.GetComponent<Rigidbody>();
        }
    }
}