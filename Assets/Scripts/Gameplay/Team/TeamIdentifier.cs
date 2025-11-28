using UnityEngine;

namespace TowerDefence.Gameplay
{
    [CreateAssetMenu(fileName = "TeamIdentifier", menuName = "TowerDefence/Gameplay/TeamIdentifier")]
    public class TeamIdentifier : ScriptableObject
    {
        [SerializeField] private string _title;
        [SerializeField] private Color _color = Color.white;
        
        public string Title => _title;
        public Color Color => _color;
    }
}
