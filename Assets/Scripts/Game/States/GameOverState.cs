using TowerDefence.Core;
using TowerDefence.Systems;
using TowerDefence.UI;
using UnityEngine;

namespace TowerDefence.Game
{
    public class GameOverState : IState
    {
        private IScreenRouter _screenRouter;
        private IUIRegistry _uiRegistry;
        private IEventBus _eventBus;
        
        private IEventToken _returnToMenuToken;

        public async void OnEnter()
        {
            _screenRouter = Services.Get<IScreenRouter>();
            _uiRegistry = Services.Get<IUIRegistry>();
            _eventBus = Services.Get<IEventBus>();
            
            _returnToMenuToken = _eventBus.Subscribe<ReturnToMenuRequestedEvent>(OnReturnToMenu);
            
            if (_uiRegistry.TryGetScreen<IScreen>("GameOver", out var screen))
            {
                await _screenRouter.PushAsync(screen);
            }
            else
            {
                Debug.LogWarning("GameOver not found. Make sure it exists in Gameplay scene with ScreenId='GameOver'");
            }
        }

        public void OnExit()
        {
            if (_returnToMenuToken != null) _eventBus.Unsubscribe(_returnToMenuToken);
        }

        public void Tick(float deltaTime) { }
        
        private async void OnReturnToMenu(ReturnToMenuRequestedEvent evt)
        {
            Time.timeScale = 1f;
            
            _uiRegistry.Clear();

            var sceneLoader = Services.Get<ISceneLoader>();
            var config = Services.Get<GameConfig>();
            await sceneLoader.LoadSceneAsync(config.MenuSceneName, UnityEngine.SceneManagement.LoadSceneMode.Single);

            var stateMachine = Services.Get<IStateMachine>();
            stateMachine.SetState(new MenuState());
        }
    }
}
