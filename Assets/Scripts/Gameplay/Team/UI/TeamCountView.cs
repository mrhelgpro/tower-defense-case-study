using System.Collections.Generic;
using TowerDefence.Gameplay;

namespace TowerDefence.UI
{
    public class TeamCountView
    {
        private readonly List<UITeamCountElement>  _teamCountElements;
        private readonly TeamCountTracker _teamCountTracker;

        private readonly Dictionary<TeamIdentifier, UITeamCountElement> _teamCountElementByIdentifier = new();
        
        public TeamCountView(List<UITeamCountElement> teamCountElements, TeamCountTracker teamCountTracker)
        {
            _teamCountElements = teamCountElements;
            _teamCountTracker = teamCountTracker;
        }

        public void Initialize()
        {
            foreach (var element in _teamCountElements)
            {
                var teamIdentifier = element.TeamIdentifier;
                var count = _teamCountTracker.GetCount(teamIdentifier);
                
                element.RefreshView(count);
                
                _teamCountElementByIdentifier.Add(teamIdentifier, element);
            }

            _teamCountTracker.OnTeamCountChanged += OnCharacterTeamChanged;
        }

        private void OnCharacterTeamChanged(TeamIdentifier previousTeamIdentifier, TeamIdentifier currentTeamIdentifier)
        {
            var previousTeamElement = _teamCountElementByIdentifier.GetValueOrDefault(previousTeamIdentifier);
            if (previousTeamElement)
            {
                var count = _teamCountTracker.GetCount(previousTeamIdentifier);
                previousTeamElement.RefreshView(count);
            }
            
            var currentTeamElement = _teamCountElementByIdentifier.GetValueOrDefault(currentTeamIdentifier);
            if (currentTeamElement)
            {
                var count = _teamCountTracker.GetCount(currentTeamIdentifier);
                currentTeamElement.RefreshView(count);
            }

            RefreshOrder();
        }

        private void RefreshOrder()
        {
            _teamCountElements.Sort((a, b) => 
            {
                var countA = _teamCountTracker.GetCount(a.TeamIdentifier);
                var countB = _teamCountTracker.GetCount(b.TeamIdentifier);
                return countB.CompareTo(countA);
            });
            
            for (var i = 0; i < _teamCountElements.Count; i++)
            {
                _teamCountElements[i].transform.SetSiblingIndex(i);
            }
        }
    }
}
