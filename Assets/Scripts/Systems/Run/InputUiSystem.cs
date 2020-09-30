using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Components;

namespace MaltaTanks
{
    sealed class InputUiSystem : IEcsRunSystem 
    {
        readonly EcsFilter<EcsUiClickEvent> _clickEvents = null; 
        readonly EcsWorld _world = null;

        private const string playAgain = "PlayAgain";
        private const string quit = "Quit";

        void IEcsRunSystem.Run () 
        {
            foreach (var i in _clickEvents)
                switch (_clickEvents.Get1(i).WidgetName)
                {
                    case playAgain:
                        _world.NewEntity().Get<GameStateSwitchEvent>().gameState = GameState.Restart;
                        break;
                    case quit:
                        _world.NewEntity().Get<GameStateSwitchEvent>().gameState = GameState.Quit;
                        break;
                }
        }
    }
}