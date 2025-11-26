using System;
using System.Threading;
using TowerDefence.Core;
using TowerDefence.Systems;
using TowerDefence.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerDefence.Game
{
    public class MenuState : IState
    {
        private IScreenRouter _screenRouter;
        private IUIRegistry _uiRegistry;
        private IEventBus _eventBus;
        private IEventToken _startGameToken;

        public async void OnEnter()
        {
            _screenRouter = Services.Get<IScreenRouter>();
            _uiRegistry = Services.Get<IUIRegistry>();
            _eventBus = Services.Get<IEventBus>();
            
            _screenRouter.Clear();
            
            _startGameToken = _eventBus.Subscribe<StartGameRequestedEvent>(OnStartGameRequested);
            
            if (_uiRegistry.TryGetScreen<MainMenuScreen>("MainMenu", out var mainMenu))
            {
                await _screenRouter.PushAsync(mainMenu);
            }
            else
            {
                Debug.LogError("MainMenuScreen not found in UIRegistry. Make sure it exists in Menu scene with ScreenId='MainMenu'");
            }
        }

        public void OnExit()
        {
            if (_eventBus != null && _startGameToken != null)
            {
                _eventBus.Unsubscribe(_startGameToken);
            }
        }

        public void Tick(float deltaTime) { }

        private async void OnStartGameRequested(StartGameRequestedEvent evt)
        {
            try
            {
                var sceneLoader = Services.Get<ISceneLoader>();
                var config = Services.Get<GameConfig>();
                
                _uiRegistry.Clear();

                await sceneLoader.LoadSceneAsync(
                    config.GameSceneName,
                    LoadSceneMode.Single,
                    CancellationToken.None
                );

                var stateMachine = Services.Get<IStateMachine>();
                stateMachine.SetState(new GameplayState());
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to start game: {ex}");
            }
        }
    }
}
