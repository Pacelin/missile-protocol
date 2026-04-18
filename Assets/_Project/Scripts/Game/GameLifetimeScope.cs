using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Project.Game
{
    public class GameLifetimeScope : LifetimeScope
    {
		[Header("Radar")]
        [SerializeField] private float _radarBeginAngle;
        [SerializeField] private float _radarSpeed;
        [Header("Ship")]
        [SerializeField] private int _durability = 8;
        [SerializeField] private int _maxDurability = 8;
        [SerializeField] private float _initialFuel;
        [SerializeField] private float _fuelConsumption;
        [SerializeField] private Vector2 _initialPosition;
        [SerializeField] private float _initialRotation;
        [Space]
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _acceleration;
        [SerializeField] private float _deceleration;
        [Space]
        [SerializeField] private float _maxAngularSpeed;
        [SerializeField] private float _angularAcceleration;
        [SerializeField] private float _angularDeceleration;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(new RadarModel(_radarBeginAngle, _radarSpeed));
            builder.RegisterInstance(new ShipModel(
                new FuelModel(_initialFuel, _fuelConsumption),
                new LinearVelocity(_maxSpeed, _acceleration, _deceleration),
                new AngularVelocity(_maxAngularSpeed, _angularAcceleration, _angularDeceleration),
                _initialPosition, _initialRotation,
                _durability, _maxDurability));
            
            builder.RegisterEntryPoint<RadarController>();
            builder.RegisterEntryPoint<ShipController>();
        }
    }
}