using Project.Core.Pause;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Project.Game
{
    public class ShipController : ITickable
    {
        private readonly ShipModel _shipModel;
        private readonly PauseController _pauseController;
        
        [Inject]
        public ShipController(ShipModel shipModel, PauseController pauseController)
        {
            _shipModel = shipModel;
            _pauseController = pauseController;
        }
        
        public void Tick()
        {
            if (_pauseController.HasAnyPause)
                return;
            
            var power = _shipModel.EnginePower;
            if (_shipModel.Fuel.Value == 0f)
                power = 0;
            
            var quaternion = Quaternion.Euler(0, 0, _shipModel.Rotation);
            var direction = (Vector2) (quaternion * Vector3.right);
            
            _shipModel.LinearVelocity.UpdateVelocity(power, direction, Time.deltaTime);
            _shipModel.AngularVelocity.UpdateAngularVelocity(_shipModel.RotateSign, Time.deltaTime);
            _shipModel.Fuel.Consume(power, Time.deltaTime);
        }
    }
}