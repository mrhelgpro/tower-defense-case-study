using UnityEngine;

namespace TowerDefence.Gameplay
{
    public class TargetTracker
    {
        private const float RADIUS_TRACKING = 4;
        private const float RADIUS_MAX_DISTANCE = 6;
        private readonly Character _character;
        
        public Character TargetCharacter { get; private set; }

        public TargetTracker(Character character)
        {
            _character = character;
        }
        
        public void Tick()
        {
            if (TargetCharacter)
            {
                if (TargetCharacter.TeamIdentifier == _character.TeamIdentifier)
                {
                    TargetCharacter = null;
                    return;
                }
                
                var distanceToTarget = Vector3.Distance(_character.transform.position, TargetCharacter.transform.position);
                if (distanceToTarget > RADIUS_MAX_DISTANCE)
                {
                    TargetCharacter = null;
                    return;
                }
            }

            var hitColliders = Physics.OverlapSphere(_character.transform.position, RADIUS_TRACKING);
            if (hitColliders.Length > 0)
            {
                foreach (var hitCollider in hitColliders)
                {
                    if (hitCollider.TryGetComponent<Character>(out var character))
                    {
                        if (character.TeamIdentifier != _character.TeamIdentifier)
                        {
                            if (TargetCharacter != character)
                            {
                                TargetCharacter = character;
                                return;
                            }
                        }
                    }
                }
            }
        }
    }
}
