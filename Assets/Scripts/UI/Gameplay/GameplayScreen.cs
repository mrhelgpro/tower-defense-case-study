using TowerDefence.Core;
using TowerDefence.Game;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence.UI
{
    public class GameplayScreen : BaseScreen
    {
        [SerializeField] private Button _pauseButton;
        
        private IEventBus _eventBus;

        protected override void Awake()
        {
            base.Awake();
            _eventBus = Services.Get<IEventBus>();
            _pauseButton.onClick.AddListener(OnPauseClicked);
        }

        private void OnPauseClicked()
        {
            _eventBus.Publish(new PauseGameRequestedEvent());
        }
        
        protected override void OnDestroy()
        {
            base.OnDestroy();
            if (_pauseButton != null) _pauseButton.onClick.RemoveListener(OnPauseClicked);
        }
    }
}