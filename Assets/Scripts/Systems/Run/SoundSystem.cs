using Leopotam.Ecs;

namespace MaltaTanks
{
    sealed class SoundSystem : IEcsRunSystem
    {
        readonly SceneInfo _sceneInfo = null;
        readonly EcsFilter<GameStateSwitchEvent> _stateSwitch = null;
        
        void IEcsRunSystem.Run () 
        {
            foreach(var i in _stateSwitch)
                switch (_stateSwitch.Get1(i).gameState)
                {
                    case GameState.Play:
                        break;
                    case GameState.Pause:
                        break;
                    case GameState.Restart:
                        break;
                    case GameState.End:
                        _sceneInfo._source.Stop();
                        _sceneInfo._source.PlayOneShot(_sceneInfo._clips[0]);
                        break;
                    case GameState.Quit:
                        break;
                }
        }
    }
}