using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

namespace TowerDefence.Gameplay
{
    public class CharacterFactory
    {
        private readonly TeamSpawnPointRepository _teamSpawnPointRepository;
        private readonly PatrolPointsHolder _patrolPointsHolder;
        private readonly CinemachineCamera _cinemachineCamera;
        private readonly TeamCountTracker _teamCountTracker;

        public CharacterFactory(
            TeamSpawnPointRepository teamSpawnPointRepository,
            PatrolPointsHolder patrolPointsHolder,
            CinemachineCamera cinemachineCamera, 
            TeamCountTracker teamCountTracker)
        {
            _teamSpawnPointRepository = teamSpawnPointRepository;
            _patrolPointsHolder = patrolPointsHolder;
            _cinemachineCamera = cinemachineCamera;
            _teamCountTracker = teamCountTracker;
        }

        public void CreatePlayer(CharacterConfig characterConfig, TeamIdentifier teamIdentifier)
        {
            var character = CreateCharacter(characterConfig, teamIdentifier);
            var controller = character.AddComponent<PlayerCharacterController>();
            controller.Construct(character);

            _cinemachineCamera.Target.TrackingTarget = character.transform;
        }

        public void CreateBot(CharacterConfig characterConfig, TeamIdentifier teamIdentifier)
        {
            var character = CreateCharacter(characterConfig, teamIdentifier);
            var controller = character.AddComponent<BotCharacterController>();
            controller.Construct(character, _patrolPointsHolder);
        }

        private Character CreateCharacter(CharacterConfig characterConfig, TeamIdentifier teamIdentifier)
        {
            var prefab = characterConfig.CharacterPrefab;
            var position = _teamSpawnPointRepository.GetSpawnPoint(teamIdentifier);
            var character = Object.Instantiate(prefab);
            
            character.transform.position = position;
            character.Construct(characterConfig, teamIdentifier);
            
            _teamCountTracker.AddCharacter(character);
            
            return character;
        }
    }
}