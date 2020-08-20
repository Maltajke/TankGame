using Leopotam.Ecs;

namespace MaltaTanks 
{
    sealed class DamageSystem : IEcsRunSystem
    {
        readonly EcsFilter<DamageEvent> _damage = null;
        readonly EcsWorld _world = null;

        void IEcsRunSystem.Run () 
        {
            foreach (var i in _damage)
            {
                ref var entity = ref _damage.Get1(i).entity;
                entity.Get<PropertyComponent>().health -= _damage.Get1(i).damage * entity.Get<PropertyComponent>().armor;
                if (entity.Get<PropertyComponent>().health <= 0)
                {
                    entity.Get<TransformComponent>().@object.SetActive(false);
                    if (entity.Has<TankComponent>())
                        _world.NewEntity().Get<GameStateSwitchEvent>().gameState = GameState.End;
                }                    
            }
        }
    }
}