using UnityEngine;

namespace Project.Game
{
    public class LinearVelocity
    {
        public Vector2 Value => _currentVelocity;
        public float MaxSpeed => _maxSpeed;
        
        private bool _enabled;
        private Vector2 _currentVelocity;
        
        private readonly float _maxSpeed;
        private readonly float _acceleration;
        private readonly float _deceleration;
        
        public LinearVelocity(float maxSpeed, float acceleration, float deceleration)
        {
            _maxSpeed = maxSpeed;
            _acceleration = acceleration;
            _deceleration = deceleration;
        }

        public void ApplyForce(Vector2 force, float deltaTime) =>
            _currentVelocity += force * deltaTime;
        
        public void UpdateVelocity(float power, Vector2 direction, float deltaTime)
        {
            power = Mathf.Clamp01(power);
            direction = direction.normalized;

            if (power > 0.001f)
            {   
                _currentVelocity = Vector2.MoveTowards(_currentVelocity,
                    direction * _maxSpeed * power, _acceleration * power * deltaTime);
            }
            else
            {
                _currentVelocity = Vector2.MoveTowards(_currentVelocity,
                    Vector2.zero, _deceleration * deltaTime);
            }
        }
    }
}