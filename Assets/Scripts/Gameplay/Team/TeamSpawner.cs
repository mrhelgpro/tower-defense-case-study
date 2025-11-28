namespace TowerDefence.Gameplay
{
    public class TeamSpawner
    {
        private readonly GameplayConfig _gameplayConfig;
        private readonly CharacterFactory _characterFactory;
        
        private bool _isPlayerSpawned;

        public TeamSpawner(GameplayConfig gameplayConfig, CharacterFactory characterFactory)
        {
            _gameplayConfig = gameplayConfig;
            _characterFactory = characterFactory;
        }

        public void Initialize()
        {
            var randomPlayerTeam = _gameplayConfig.GetRandomTeamIdentifier();
            
            foreach (var teamIdentifier in _gameplayConfig.TeamIdentifiers)
            {
                for (var i = 0; i < _gameplayConfig.TeamCharactersAmount; i++)
                {
                    var randomCharacterConfig = _gameplayConfig.GetRandomCharacterConfig();
                    
                    var canPlayerSpawn = teamIdentifier == randomPlayerTeam && !_isPlayerSpawned;
                    if (canPlayerSpawn)
                    {
                        _characterFactory.CreatePlayer(randomCharacterConfig, teamIdentifier);
                        _isPlayerSpawned = true;
                    }
                    else
                    {
                        _characterFactory.CreateBot(randomCharacterConfig, teamIdentifier);
                    }
                }
            }
        }
    }
}
