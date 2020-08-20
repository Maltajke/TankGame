using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MaltaTanks 
{
    sealed class InputEventSystem : IEcsInitSystem, IEcsDestroySystem
    {
        readonly ActionMap _input = null;
        readonly EcsWorld _world = null;       

        public void Init()
        {
            _input.Player.GunSwitchPlus.started += gunSwitchCallbackPlus;
            _input.Player.GunSwitchMinus.started += gunSwitchCallbackMinus;
            _input.Player.Fire.started += fireCallback;
        }

        public void Destroy()
        {
            _input.Player.GunSwitchPlus.started -= gunSwitchCallbackPlus;
            _input.Player.GunSwitchMinus.started -= gunSwitchCallbackMinus;
            _input.Player.Fire.started -= fireCallback;
        }

        private void fireCallback(InputAction.CallbackContext ctx)
        {
            _world.NewEntity().Get<ShootingEvent>();
        }

        private void gunSwitchCallbackPlus(InputAction.CallbackContext ctx)
        {
            gunSwitchEvent(GunSwitchSide.Plus);
        }

        private void gunSwitchCallbackMinus(InputAction.CallbackContext ctx)
        {
            gunSwitchEvent(GunSwitchSide.Minus);
        }

        private void gunSwitchEvent(GunSwitchSide side)
        {
            _world.NewEntity().Get<GunSwitchEvent>().switchSide = side;
        }
    }
}