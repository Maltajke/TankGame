using Leopotam.Ecs;
using MaltaTanks.Extentions;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MaltaTanks 
{
    sealed class EnemyInitSystem : IEcsInitSystem
    {       
        readonly EcsWorld _world = null;
        readonly SceneInfo _sceneInfo = null;    

        public void Init () 
        {
            var enemiesPool = new GameObject("[ENEMIES]");
            for (int i = 0; i < _sceneInfo._gameProfile.maxEnemies; i++)
            {
                int index = Random.Range(0, _sceneInfo._enemies.Count);
                var enemy = _world.NewEntity();
                ref var enemyTransform = ref enemy.Get<TransformComponent>();
                ref var enemyPropreties = ref enemy.Get<PropertyComponent>();
                ref var enemyComponent = ref enemy.Get<EnemyComponent>();

                enemyPropreties = _sceneInfo._enemies[index].properties;

                enemyTransform.@object = Object.Instantiate(_sceneInfo._enemies[index].prefab, enemiesPool.transform);
                enemyTransform.transform = enemyTransform.@object.GetComponent<Transform>();
                enemyTransform.rigidbody = enemyTransform.@object.GetComponent<Rigidbody>();
                enemyTransform.transform.position = NavMeshExtentions.GetRandomPoint(Vector3.zero, 70, 8);

                enemyComponent.agent = enemyTransform.@object.GetComponent<NavMeshAgent>();
                enemyComponent.agent.speed = enemyPropreties.speed;
                enemyComponent.agent.enabled = true;

                enemyTransform.@object.SetActive(false);

                if (!enemyTransform.@object.GetComponent<HitTrigger>())
                    enemyTransform.@object.AddComponent<HitTrigger>();
                enemyTransform.@object.GetComponent<HitTrigger>()._world = _world;

                if(enemyPropreties.weapons.Count != 0)
                    enemyPropreties.currentWeapon = enemyPropreties.weapons[0];
            }
        }
    }
}