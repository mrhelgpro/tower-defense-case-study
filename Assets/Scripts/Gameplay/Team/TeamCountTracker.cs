using System;
using System.Collections.Generic;

namespace TowerDefence.Gameplay
{
    public class TeamCountTracker
    {
        public event Action<TeamIdentifier,TeamIdentifier> OnTeamCountChanged;
        public event Action OnTeamComplete;
        
        private readonly Dictionary<TeamIdentifier, int> _teamCounts = new ();

        private int _totalCharacters;

        public void AddCharacter(Character character)
        {
            _totalCharacters++;
            IncreaseTeam(character.TeamIdentifier);
            
            character.OnTeamChanged += OnCharacterTeamChanged;
        }

        public int GetCount(TeamIdentifier teamIdentifier)
        {
            return _teamCounts.GetValueOrDefault(teamIdentifier);
        }

        private void OnCharacterTeamChanged(TeamIdentifier previousTeamIdentifier, TeamIdentifier currentTeamIdentifier)
        {
            DecreaseTeam(previousTeamIdentifier);
            IncreaseTeam(currentTeamIdentifier);
            
            OnTeamCountChanged?.Invoke(previousTeamIdentifier, currentTeamIdentifier);
            
            if (_teamCounts.TryGetValue(currentTeamIdentifier, out var currentTeamCount) && currentTeamCount == _totalCharacters)
            {
                OnTeamComplete?.Invoke();
            }
        }

        private void IncreaseTeam(TeamIdentifier teamIdentifier)
        {
            var current = _teamCounts.GetValueOrDefault(teamIdentifier, 0);
            _teamCounts[teamIdentifier] = ++current;
        }

        private void DecreaseTeam(TeamIdentifier teamIdentifier)
        {
            if (_teamCounts.TryGetValue(teamIdentifier, out var current))
            {
                _teamCounts[teamIdentifier] = --current;
            }
        }
    }
}