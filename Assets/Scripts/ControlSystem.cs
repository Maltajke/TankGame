using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaltaTanks 
{
    sealed class ControlSystem : MonoBehaviour 
    {
        EcsWorld _world;
        EcsSystems _systemsFixed;
        EcsSystems _systems;
        ActionMap _input;

        [SerializeField] private SceneInfo _sceneInfo = null;  

        void Start ()
        {
            // void can be switched to IEnumerator for support coroutines.
            
            _world = new EcsWorld ();
            _systems = new EcsSystems (_world);
            _systemsFixed = new EcsSystems(_world);
            _input = new ActionMap();
            _input.Enable();

#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create (_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create (_systems);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systemsFixed);
#endif
            
            _systemsFixed
               .Add(new TankMoveSystem())
               .Inject(_input)               
               .Init();

            _systems     //order is important!!
               //init
               .Add(new TankTransformInitSystem())
               .Add(new TankPropertiesInitSystem())
               .Add(new PoolInitSystem())
               .Add(new EnemyInitSystem())
               .Add(new InputEventSystem())

               //run
               .Add(new InputUpdateSystem())              
               .Add(new ShootingSystem())
               .Add(new GunSwitchSystem())
               .Add(new CollisionHandleSystem())               
               .Add(new EnemySpawnSystem())               
               .Add(new DamageSystem())               
               .Add(new UiViewSystem())               
               .Add(new InputUiSystem())
               .Add(new SoundSystem())
               .Add(new GameStateSystem())


               .OneFrame<CollisionEvent>()
               .OneFrame<DamageEvent>()               
               .OneFrame<GunSwitchEvent>()
               .OneFrame<ShootingEvent>()
               .OneFrame<GameStateSwitchEvent>()

               .Inject(_input)
               .Inject(_sceneInfo)        
               .InjectUi(_sceneInfo._ui)
               .Init();        
        }

        void Update () 
        {            
            _systems?.Run();           
        }

        void FixedUpdate()
        {
            _systemsFixed?.Run();
        }

        void OnDestroy ()
        {
            if (_systems != null && _systemsFixed != null) 
            {
                _systemsFixed.Destroy();
                _systems.Destroy ();                
                _systemsFixed = null;
                _systems = null;                
                _world.Destroy ();
                _world = null;
            }
        }
    }
}