using System;
using Project.Game.Map;
using UnityEngine;
using UnityEngine.Assertions;

namespace Project.Game
{
    public class ShipModel : IMapUnit
    {
        public event Action OnTransformChanged;
        public event Action OnDamage;
        public event Action OnHeal;
        public event Action OnDie;
        
        public FuelModel Fuel => _fuel;
        public LinearVelocity LinearVelocity => _linearVelocity; 
        public AngularVelocity AngularVelocity => _angularVelocity;
        
        public float EnginePower => _enginePower;
        public int RotateSign => _rotateSign;
        public Vector2 Position => _position;
        public float Rotation => _rotation;
        public int Durability => _durability;
        public int MaxDurability => _maxDurability;

        private int _durability;
        private float _enginePower;
        private int _rotateSign;
        private Vector2 _position;
        private float _rotation;

        private int _maxDurability;
        private readonly FuelModel _fuel;
        private readonly LinearVelocity _linearVelocity;
        private readonly AngularVelocity _angularVelocity;
        
        public ShipModel(
            FuelModel fuel, LinearVelocity linearVelocity, AngularVelocity angularVelocity,
            Vector2 initialPosition, float initialRotation,
            int initialDurability, int maxDurability)
        {
            _fuel = fuel;
            _linearVelocity = linearVelocity;
            _angularVelocity = angularVelocity;
            _maxDurability = maxDurability;
            _durability = initialDurability;
            _position = initialPosition;
            _rotation = initialRotation;
        }

        public void TakeDamage(int amount)
        {
            if (_durability == 0)
                return;
            
            Assert.IsTrue(amount > 0);
            _durability = Mathf.Clamp(_durability - amount, 0, _maxDurability);
            OnDamage?.Invoke();
            
            if (_durability == 0)
                OnDie?.Invoke();
        }

        public void Heal(int amount)
        {
            Assert.IsTrue(amount > 0);
            _durability = Mathf.Clamp(_durability + amount, 0, _maxDurability);
            OnHeal?.Invoke();
        }
        
        public void SetEnginePower(float power) => _enginePower = Mathf.Clamp01(power);
        public void SetRotateSign(int sign) =>_rotateSign = sign;

        public void MoveRotate(Vector2 positionDelta, float rotationDelta)
        {
            _position += positionDelta;
            _rotation += rotationDelta;
            OnTransformChanged?.Invoke();
        }
    }
}