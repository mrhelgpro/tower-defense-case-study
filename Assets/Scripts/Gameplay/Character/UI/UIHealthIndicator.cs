using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence.Gameplay
{
    public class UIHealthIndicator : MonoBehaviour
    {
        [SerializeField] private Character _character;
        [SerializeField] private Slider _slider;
        [SerializeField] private Image _image;

        private void OnEnable()
        {
            _character.HealthPointsRuntime.OnHealthPointsChanged += OnHealthPointsChanged;
            _character.OnTeamChanged += OnTeamChanged;
        }
        
        private void OnDisable()
        {
            _character.HealthPointsRuntime.OnHealthPointsChanged -= OnHealthPointsChanged;
            _character.OnTeamChanged -= OnTeamChanged;
        }

        private void LateUpdate()
        {
            transform.rotation = Quaternion.identity;
        }
        
        private void OnHealthPointsChanged()
        {
            _slider.value = _character.HealthPointsRuntime.CurrentHealth / _character.HealthPointsRuntime.MaxHealth;
        }
        
        private void OnTeamChanged(TeamIdentifier previousTeamIdentifier, TeamIdentifier currentTeamIdentifier)
        {
            _image.color = _character.TeamIdentifier.Color;
        }
    }
}

