using Leopotam.Ecs;

namespace MaltaTanks
{
    sealed class TankPropertiesInitSystem : IEcsInitSystem 
    {
        readonly EcsFilter<TankComponent> _tank = null;
        readonly SceneInfo _sceneInfo = null;

        public void Init () 
        {
            foreach(var i in _tank)
            {
                ref var props = ref _tank.GetEntity(i).Get<PropertyComponent>();
                props = _sceneInfo._tank.properties;
                foreach(var weapon in props.weapons)
                    if (weapon.prefab.activeSelf)
                        props.currentWeapon = weapon;
                props.health = props.startHealth;
            }
        }
    }
}