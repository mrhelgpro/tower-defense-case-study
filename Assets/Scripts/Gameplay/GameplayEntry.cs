using System.Collections.Generic;
using TowerDefence.Core;
using TowerDefence.Game;
using TowerDefence.UI;
using Unity.Cinemachine;
using UnityEngine;

namespace TowerDefence.Gameplay
{
    public class GameplayEntry : MonoBehaviour
    {
        [SerializeField] private GameplayConfig _gameplayConfig;
        [SerializeField] private TeamSpawnPointRepository _teamSpawnPointRepository;
        [SerializeField] private CinemachineCamera _cinemachineCamera;
        [SerializeField] private List<Transform> _patrolPoints = new();
        [SerializeField] private List<UITeamCountElement> _teamCountElements = new();
        
        private PatrolPointsHolder  _patrolPointsHolder;
        private CharacterFactory  _characterFactory;
        private TeamSpawner _teamSpawner;
        private TeamCountTracker _teamCountTracker;
        private TeamCountView _teamCountView;

        private void Awake()
        {
            _teamCountTracker = new TeamCountTracker();
            _teamCountView = new TeamCountView(_teamCountElements, _teamCountTracker);
            _patrolPointsHolder = new PatrolPointsHolder(_patrolPoints, _gameplayConfig.TeamIdentifiers);
            _characterFactory = new CharacterFactory(_teamSpawnPointRepository, _patrolPointsHolder, _cinemachineCamera, _teamCountTracker);
            _teamSpawner = new TeamSpawner(_gameplayConfig, _characterFactory);
            
            _teamSpawnPointRepository.Initialize();
            _patrolPointsHolder.Initialize();
            _teamSpawner.Initialize();
            _teamCountView.Initialize();
            
            _teamCountTracker.OnTeamComplete += OnTeamComplete;
        }

        private void OnDestroy()
        {
            _teamCountTracker.OnTeamComplete -= OnTeamComplete;
        }

        private void OnTeamComplete()
        {
            var eventBus = Services.Get<IEventBus>();
            eventBus.Publish(new GameOverEvent());
        }
    }
}
