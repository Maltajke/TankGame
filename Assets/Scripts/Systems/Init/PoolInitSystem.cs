using Leopotam.Ecs;
using MaltaTanks.Extentions;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MaltaTanks 
{
    sealed class PoolInitSystem : IEcsInitSystem
    {
        readonly EcsFilter<TankComponent, PropertyComponent> _tank = null;        
        readonly EcsWorld _world = null;

        public void Init () 
        {
            var poolsParent = new GameObject("[POOLS]");
            
            //bullets init
            foreach (var i in _tank)
            {                
                foreach(var weapon in _tank.Get2(i).weapons)
                {
                    ref var pool = ref _world.NewEntity().Get<PoolComponent>();
                    var bul = weapon.bulletType;                    
                    pool.bullets = new List<BulletProfileComponent>();
                    var bulParent = new GameObject(bul.profile.prefab.name);
                    bulParent.transform.SetParent(poolsParent.transform);
                    for (int j = 0; j < bul.count; j++)
                    {
                        var imBullet = Object.Instantiate(bul.profile.prefab, bulParent.transform);
                        if (!imBullet.GetComponent<HitTrigger>())
                            imBullet.AddComponent<HitTrigger>();
                        imBullet.GetComponent<HitTrigger>()._world = _world; 
                        imBullet.SetActive(false);
                        var bullet = bul.profile;                      
                        bullet.prefab = imBullet;
                        pool.bullets.Add(bullet);
                    }
                }
            }
        }
    }
}