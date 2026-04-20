using System;
using Plugins.Audio;
using Project.Core.Pause;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Project.Game
{
    public class ShipController : IInitializable, IDisposable, ITickable
    {
        private SoundEventInstance _soundEventInstance;
        private readonly ShipModel _shipModel;
        private readonly PauseController _pauseController;
        
        [Inject]
        public ShipController(ShipModel shipModel, PauseController pauseController)
        {
            _shipModel = shipModel;
            _pauseController = pauseController;
        }

        public void Initialize()
        {
            _soundEventInstance = AudioSystem.Game_Map_ShipEngine.CreateInstance();
            _soundEventInstance.Start();
        }

        public void Dispose()
        {
            _soundEventInstance.Stop(true);
            _soundEventInstance.Release();
            _soundEventInstance = null;
        }
        
        public void Tick()
        {
            if (_pauseController.HasAnyPause)
            {
                AudioSystem.Global.SetEnginePower(0);
                return;
            }
            
            var power = _shipModel.EnginePower;
            if (_shipModel.Fuel.Value == 0f)
                power = 0;
            
            if (_shipModel.Fuel.Value == 0f &&
                _shipModel.LinearVelocity.Value.sqrMagnitude <= 0.001f)
                G.ResultCanvas.Show(false);
            
            AudioSystem.Global.SetEnginePower(power);
            var quaternion = Quaternion.Euler(0, 0, _shipModel.Rotation);
            var direction = (Vector2) (quaternion * Vector3.up);
            
            _shipModel.LinearVelocity.UpdateVelocity(power, direction, Time.deltaTime);
            _shipModel.AngularVelocity.UpdateAngularVelocity(_shipModel.RotateSign, Time.deltaTime);
            _shipModel.Fuel.Consume(power, Time.deltaTime);

            var deltaPosition = _shipModel.LinearVelocity.Value * Time.deltaTime;
            var deltaRotation = _shipModel.AngularVelocity.Value * Time.deltaTime;
            
            _shipModel.MoveRotate(deltaPosition, deltaRotation);
        }
    }
}