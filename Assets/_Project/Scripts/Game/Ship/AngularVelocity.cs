using UnityEngine;

namespace Project.Game
{
    public class AngularVelocity
    {
        public float Value => _currentAngularVelocity;

        private float _currentAngularVelocity;
        private readonly float _maxAngularSpeed;
        private readonly float _angularAcceleration;
        private readonly float _angularDeceleration;

        public AngularVelocity(float maxAngularSpeed, float angularAcceleration, float angularDeceleration)
        {
            _maxAngularSpeed = maxAngularSpeed;
            _angularAcceleration = angularAcceleration;
            _angularDeceleration = angularDeceleration;
        }

        public void UpdateAngularVelocity(int directionSign, float deltaTime)
        {
            if (directionSign != 0)
                _currentAngularVelocity = Mathf.MoveTowards(_currentAngularVelocity,
                    _maxAngularSpeed * directionSign, _angularAcceleration * deltaTime);
            else
                _currentAngularVelocity = Mathf.MoveTowards(_currentAngularVelocity, 
                    0, _angularDeceleration * deltaTime);
        }
    }
}