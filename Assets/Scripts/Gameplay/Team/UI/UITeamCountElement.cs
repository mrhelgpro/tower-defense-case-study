using DG.Tweening;
using TMPro;
using TowerDefence.Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence.UI
{
    public class UITeamCountElement : MonoBehaviour
    {
        [SerializeField] private TeamIdentifier _teamIdentifier;
        [SerializeField] private Image _colorImage;
        [SerializeField] private TMP_Text _countText;
        
        public TeamIdentifier TeamIdentifier => _teamIdentifier;

        public void RefreshView(int count)
        {
            var isActive = count > 0;
            if (isActive)
            {
                _colorImage.color = _teamIdentifier.Color;
                _countText.text = count.ToString();

                ShowPopupAnimation();
            }
            
            gameObject.SetActive(isActive);
        }
        
        private void ShowPopupAnimation()
        {
            transform.DOKill();
            transform.localScale = Vector3.one;
                
            transform
                .DOScale(1.2f, 0.15f)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    transform .DOScale(1f, 0.15f).SetEase(Ease.InQuad);
                });
        }
    }
}
