using System;

namespace Project.Game
{
    public class RadarModel
    {
        public event Action<float, float> OnAngleChanged;
        
        public float Angle => _angle;
        
        private float _angle;
        
        private readonly float _speed;

        public RadarModel(float startAngle, float speed)
        {
            _angle = startAngle;
            _speed = speed;
        }

        public void Tick(float deltaTime)
        {
            var firstAngle = _angle;
            _angle += _speed * deltaTime;
            
            OnAngleChanged?.Invoke(firstAngle, _angle);
        }
    }
}