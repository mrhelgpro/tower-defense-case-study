using TowerDefence.Core;
using TowerDefence.Game;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence.UI
{
    public class PauseScreen : BaseScreen
    {
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _returnToMenuButton;
        
        private IEventBus _eventBus;

        protected override void Awake()
        {
            base.Awake();
            _eventBus = Services.Get<IEventBus>();
            _resumeButton.onClick.AddListener(OnResumeClicked);
            _returnToMenuButton.onClick.AddListener(OnReturnToMenuClicked);
        }
        
        private void OnResumeClicked()
        {
            _eventBus.Publish(new ResumeGameRequestedEvent());
        }
        
        private void OnReturnToMenuClicked()
        {
            _eventBus.Publish(new ReturnToMenuRequestedEvent());
        }
        
        protected override void OnDestroy()
        {
            base.OnDestroy();
            if (_resumeButton != null) _resumeButton.onClick.RemoveListener(OnResumeClicked);
            if (_returnToMenuButton != null) _returnToMenuButton.onClick.RemoveListener(OnReturnToMenuClicked);
        }
    }
}
