using Leopotam.Ecs;

namespace MaltaTanks 
{
    sealed class GunSwitchSystem : IEcsRunSystem
    {
        readonly EcsFilter<TankComponent, PropertyComponent> _tank = null;
        readonly EcsFilter<GunSwitchEvent> _switchEvent = null;

        public void Run()
        {
            foreach (var i in _switchEvent)
                SwitchGun(_switchEvent.Get1(i).switchSide);
        }

        private void SwitchGun(GunSwitchSide side)
        {   
            foreach (var i in _tank)
            {
                ref var guns = ref _tank.Get2(i).weapons;
                _tank.Get2(i).currentWeapon.prefab.SetActive(false);
                int index = guns.IndexOf(_tank.Get2(i).currentWeapon);
                int lastIndex = guns.Count - 1;

                switch (side)
                {
                    case GunSwitchSide.Plus:
                        if (index < lastIndex)
                            index += 1;
                        else
                            index = 0;
                        break;
                    case GunSwitchSide.Minus:
                        if (index > 0)
                            index -= 1;
                        else
                            index = lastIndex;
                        break;
                }

                _tank.Get2(i).currentWeapon = guns[index];
                _tank.Get2(i).currentWeapon.prefab.SetActive(true);
            }
        }
    }
}