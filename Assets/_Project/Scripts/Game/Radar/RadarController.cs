using System;
using Project.Core.Pause;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Project.Game
{
    public class RadarController : ITickable
    {
        private readonly PauseController _pauseController;
        private readonly RadarModel _radarModel;
        
        [Inject]
        public RadarController(PauseController pauseController, RadarModel radarModel)
        {
            _pauseController = pauseController;
            _radarModel = radarModel;
        }
        
        public void Tick()
        {
            if (_pauseController.HasAnyPause)
                return;
            _radarModel.Tick(Time.deltaTime);
        }
    }
}