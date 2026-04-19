using Project.Core.Pause;
using UnityEngine;
using VContainer;

namespace Project.Game
{
    public class G : MonoBehaviour
    {
        public static PauseController PauseController => _instance._pauseController;
        public static RadarModel RadarModel => _instance._radarModel;
        public static ShipModel ShipModel => _instance._shipModel;
        public static Main Main => _instance._main;
        
        [Inject] private PauseController _pauseController;
        [Inject] private RadarModel _radarModel;
        [Inject] private ShipModel _shipModel;

        [SerializeField] private Main _main;
        
        private static G _instance;

        [Inject]
        private void Construct() => _instance = this;
    }
}