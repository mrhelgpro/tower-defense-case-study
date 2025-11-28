using System.Collections.Generic;
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
        
        private PatrolPointsHolder  _patrolPointsHolder;
        private CharacterFactory  _characterFactory;
        private TeamSpawner _teamSpawner;

        private void Awake()
        {
            _patrolPointsHolder = new PatrolPointsHolder(_patrolPoints, _gameplayConfig.TeamIdentifiers);
            _characterFactory = new CharacterFactory(_teamSpawnPointRepository, _patrolPointsHolder, _cinemachineCamera);
            _teamSpawner = new TeamSpawner(_gameplayConfig, _characterFactory);
            
            _teamSpawnPointRepository.Initialize();
            _patrolPointsHolder.Initialize();
            _teamSpawner.Initialize();
        }
    }
}
