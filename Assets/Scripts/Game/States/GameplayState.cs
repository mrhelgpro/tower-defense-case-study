using TowerDefence.Core;
using TowerDefence.Systems;
using TowerDefence.UI;
using UnityEngine;

namespace TowerDefence.Game
{
    public class GameplayState : IState
    {
        private IStateMachine _stateMachine;
        private IScreenRouter _screenRouter;
        private IUIRegistry _uiRegistry;
        private IEventBus _eventBus;
        
        private IEventToken _pauseToken;
        private IEventToken _resumeToken;
        private IEventToken _gameOverToken;
        private IEventToken _returnToMenuToken;

        public async void OnEnter()
        {
            _stateMachine = Services.Get<IStateMachine>();
            _screenRouter = Services.Get<IScreenRouter>();
            _uiRegistry= Services.Get<IUIRegistry>();
            _eventBus = Services.Get<IEventBus>();
            
            _screenRouter.Clear();
            
            _pauseToken = _eventBus.Subscribe<PauseGameRequestedEvent>(OnPauseRequested);
            _resumeToken = _eventBus.Subscribe<ResumeGameRequestedEvent>(OnResumeRequested);
            _gameOverToken = _eventBus.Subscribe<GameOverEvent>(OnGameOver);
            _returnToMenuToken = _eventBus.Subscribe<ReturnToMenuRequestedEvent>(OnReturnToMenu);
            
            if (_uiRegistry.TryGetScreen<IScreen>("GameplayHUD", out var hud))
            {
                await _screenRouter.PushAsync(hud);
            }
            else
            {
                Debug.LogWarning("GameplayHUD not found. Make sure it exists in Gameplay scene with ScreenId='GameplayHUD'");
            }
        }

        public void OnExit()
        {
            if (_eventBus == null)
            {
                return;
            }

            if (_pauseToken != null) _eventBus.Unsubscribe(_pauseToken);
            if (_resumeToken != null) _eventBus.Unsubscribe(_resumeToken);
            if (_gameOverToken != null) _eventBus.Unsubscribe(_gameOverToken);
            if (_returnToMenuToken != null) _eventBus.Unsubscribe(_returnToMenuToken);
        }

        public void Tick(float deltaTime){}

        private async void OnPauseRequested(PauseGameRequestedEvent evt)
        {
            Time.timeScale = 0f;
            
            if (!_uiRegistry.TryGetScreen<IScreen>("Pause", out var pauseScreen))
            {
                return;
            }
            
            await _screenRouter.ShowModalAsync(pauseScreen);
        }

        private async void OnResumeRequested(ResumeGameRequestedEvent evt)
        {
            Time.timeScale = 1f;
            
            await _screenRouter.HideModalAsync();
        }
        
        private async void OnGameOver(GameOverEvent evt)
        {
            Time.timeScale = 0f;
            
            _stateMachine.SetState(new GameOverState());
        }
        
        private async void OnReturnToMenu(ReturnToMenuRequestedEvent evt)
        {
            Time.timeScale = 1f;
            
            _uiRegistry.Clear();

            var sceneLoader = Services.Get<ISceneLoader>();
            var config = Services.Get<GameConfig>();
            await sceneLoader.LoadSceneAsync(config.MenuSceneName, UnityEngine.SceneManagement.LoadSceneMode.Single);
            
            _stateMachine.SetState(new MenuState());
        }
    }
}
