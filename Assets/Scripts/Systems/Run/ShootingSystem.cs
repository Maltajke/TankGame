using Leopotam.Ecs;
using UnityEngine;

namespace MaltaTanks 
{
    sealed class ShootingSystem :  IEcsRunSystem
    {        
        readonly EcsFilter<TankComponent, PropertyComponent> _tank = null;
        readonly EcsFilter<PoolComponent> _pool = null;
        readonly EcsFilter<ShootingEvent> _shot = null;
        readonly EcsFilter<GameStateComponent> _state  = null;

        public void Run() 
        {
            if(!_shot.IsEmpty() && _state.Get1(0).gameState == GameState.Play)
                Fire(); //spawn bullet

            foreach (var i in _pool) // Update bullet position 
                foreach (var bul in _pool.Get1(i).bullets)
                    if (bul.prefab.activeSelf)
                        bul.prefab.transform.Translate(Vector3.forward * bul.speed * Time.deltaTime);
        }

        private void Fire() 
        {
            WeaponComponent weapon = _tank.Get2(0).currentWeapon;
            
            foreach (var i in _pool)
                foreach (var bul in _pool.Get1(i).bullets)
                    if (bul.gunType == weapon.gunType)
                        if (!bul.prefab.activeSelf)
                        {
                            bul.prefab.SetActive(true);
                            bul.prefab.transform.SetPositionAndRotation(weapon.bulletStart.position, weapon.bulletStart.rotation);                    
                            break;
                        }
        }
    }
}