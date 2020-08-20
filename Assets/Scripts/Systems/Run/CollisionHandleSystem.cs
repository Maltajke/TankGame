using Leopotam.Ecs;

namespace MaltaTanks 
{
    sealed class CollisionHandleSystem : IEcsRunSystem 
    {
        readonly EcsFilter<CollisionEvent> _hit = null;
        readonly EcsFilter<PoolComponent> _pools = null;
        readonly EcsFilter<EnemyComponent, TransformComponent, PropertyComponent> _enemies = null;                
        readonly EcsFilter<TankComponent, PropertyComponent> _tank = null;
        readonly EcsWorld _world = null;

        void IEcsRunSystem.Run ()
        {
            foreach(var i in _hit)
                switch (_hit.Get1(i).emitter.tag)
                {
                    case "Bullet":
                        foreach (var p in _pools)
                            foreach (var bull in _pools.Get1(p).bullets)
                                if (bull.prefab == _hit.Get1(i).emitter)
                                    foreach (var e in _enemies)
                                        if (_enemies.Get2(e).@object == _hit.Get1(i).hitCollider.gameObject)
                                        {
                                            ref var damageEvent = ref _world.NewEntity().Get<DamageEvent>();
                                            damageEvent.damage = bull.damage;
                                            damageEvent.entity = _enemies.GetEntity(e);
                                            break;
                                        }
                        _hit.Get1(i).emitter.SetActive(false);
                        break;

                    case "Enemy":
                        foreach (var e in _enemies)
                            if (_hit.Get1(i).hitCollider.tag == "Player")
                                if (_enemies.Get2(e).@object == _hit.Get1(i).emitter)
                                {
                                    ref var damageEvent = ref _world.NewEntity().Get<DamageEvent>();
                                    damageEvent.damage = _enemies.Get3(e).currentWeapon.bulletType.profile.damage;
                                    damageEvent.entity = _tank.GetEntity(0);
                                    _hit.Get1(i).emitter.SetActive(false);
                                    break;
                                }
                        break;

                    default:
                        break;
                }
        }
    }
}