using TowerDefence.Core;
using TowerDefence.Game;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence.UI
{
    public class GameOverScreen : BaseScreen
    {
        [SerializeField] private Button _returnToMenuButton;
        
        private IEventBus _eventBus;

        protected override void Awake()
        {
            base.Awake();
            _eventBus = Services.Get<IEventBus>();
            _returnToMenuButton.onClick.AddListener(OnReturnToMenuClicked);
        }
        
        private void OnReturnToMenuClicked()
        {
            _eventBus.Publish(new ReturnToMenuRequestedEvent());
        }
        
        protected override void OnDestroy()
        {
            base.OnDestroy();
            if (_returnToMenuButton != null) _returnToMenuButton.onClick.RemoveListener(OnReturnToMenuClicked);
        }
    }
}
