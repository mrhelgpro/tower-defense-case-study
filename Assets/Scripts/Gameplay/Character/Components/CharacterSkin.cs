using DG.Tweening;
using UnityEngine;

namespace TowerDefence.Gameplay
{
    public class CharacterSkin : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        
        private Color _defaultColor;
        
        public void ShowTeamColor(Color color)
        {
            _defaultColor = color;
            _meshRenderer.material.color = color;
        }

        public void ShowDamageReceived()
        {
            _meshRenderer.material.DOKill();
            _meshRenderer.material
                .DOColor(Color.white, 0.1f)
                .OnComplete(() =>
                {
                    _meshRenderer.material
                        .DOColor(_defaultColor, 0.1f);
                });
        }
    }
}
