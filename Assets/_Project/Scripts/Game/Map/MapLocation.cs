using System.Collections.Generic;
using UnityEngine;

namespace Project.Game.Map
{
    public class MapLocation : MonoBehaviour
    {
        public static IReadOnlyList<MapLocation> AvailableLocations => _availableLocations;
        private static List<MapLocation> _availableLocations = new List<MapLocation>();
        
        public Vector2 Position => (Vector2)transform.position;
        public Color RadarColor => _radarColor;
        public float RadarSize => _radarSize;
        public bool CanLockOn => _canLockOn;
        public bool ShowOnRadar => _showOnRadar;
        
        [SerializeField] private Color _radarColor;
        [SerializeField] private float _radarSize;
        [SerializeField] private bool _showOnRadar = true;
        [SerializeField] private bool _canLockOn;

        private void Awake() => _availableLocations.Add(this);
        private void OnDestroy() => _availableLocations.Remove(this);
    }
}