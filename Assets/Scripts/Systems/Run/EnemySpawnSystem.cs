using Leopotam.Ecs;
using UnityEngine;
using MaltaTanks.Extentions;

namespace MaltaTanks 
{
    sealed class EnemySpawnSystem : IEcsRunSystem, IEcsInitSystem
    {
        readonly EcsFilter<EnemyComponent, TransformComponent, PropertyComponent> _enemies = null;
        readonly EcsFilter<TankComponent, TransformComponent> _tank = null;
        readonly SceneInfo _sceneInfo = null;

        private float startTime;

        public void Init()
        {
            startTime = Time.time;
        }

        void IEcsRunSystem.Run () 
        {
            foreach (var i in _enemies)
            {
                if (_enemies.Get2(i).@object.activeSelf)
                    _enemies.Get1(i).agent.SetDestination(_tank.Get2(0).transform.position);

                if (!_enemies.Get2(i).@object.activeSelf && Time.time - startTime >= _sceneInfo._gameProfile.spawnDelay)
                {
                    _enemies.Get2(i).transform.position = NavMeshExtentions.GetRandomPoint(Vector3.zero, 70, 8);
                    _enemies.Get3(i).health = _enemies.Get3(i).startHealth;
                    _enemies.Get2(i).@object.SetActive(true);
                    startTime = Time.time;
                    break;
                }
            }
        }        
    }
}