using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MaltaTanks 
{
    sealed class GameStateSystem : IEcsRunSystem, IEcsInitSystem
    {
        // auto-injected fields.        
        readonly EcsFilter<GameStateComponent> _state = null;
        readonly EcsFilter<GameStateSwitchEvent> _stateSwitch = null;
        readonly EcsWorld _world = null;

        public void Init()
        {
            Time.timeScale = 1;
            _world.NewEntity().Get<GameStateComponent>().gameState = GameState.Play;
        }

        void IEcsRunSystem.Run () 
        {
            foreach (var s in _stateSwitch)
                foreach (var i in _state)
                {
                    _state.Get1(i).gameState = _stateSwitch.Get1(s).gameState;
                    switch (_state.Get1(i).gameState)
                    {
                        case GameState.Play:
                            Time.timeScale = 1;
                            break;
                        case GameState.Pause:
                            Time.timeScale = 0;
                            break;
                        case GameState.End:
                            Time.timeScale = 0;
                            break;
                        case GameState.Quit:
                            Application.Quit();
                            break;
                        case GameState.Restart:
                            SceneManager.LoadScene(1);
                            break;
                    }
                }
        }        
    }
}