using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Systems;
using UnityEngine;
using UnityEngine.UI;
using MaltaTanks.Extentions;

namespace MaltaTanks
{
    sealed class UiViewSystem : IEcsRunSystem, IEcsInitSystem
    {
        readonly SceneInfo _sceneInfo = null;
        readonly EcsFilter<TankComponent, TransformComponent, PropertyComponent> _tank = null;
        readonly EcsFilter<GameStateSwitchEvent> _state = null;

        [EcsUiNamed("HealthBar")] Slider _health = null;
        [EcsUiNamed("GameOverTab")] GameObject _endGame = null;

        public void Init()
        {
            _endGame.SetActive(false);
        }

        void IEcsRunSystem.Run () 
        {
            if (_tank.Get2(0).@object.activeSelf)
            {
                _health.transform.position = _sceneInfo._cam.WorldToScreenPoint(_tank.Get2(0).transform.position + new Vector3(0, 0, 2.5f));
                _health.value = _tank.Get3(0).health.Remap(0, _tank.Get3(0).startHealth, 0, 1);
            }
            else _health.gameObject.SetActive(false);            

            foreach(var i in _state)
                switch (_state.Get1(i).gameState)
                {
                    case GameState.Play:
                        break;
                    case GameState.Pause:
                        break;
                    case GameState.End:
                        _endGame.SetActive(true);
                        break;
                    case GameState.Quit:
                        break;
                }
        }
    }
}