using System;
using UnityEngine;

namespace Project.MainMenu
{
    public static class Measurement
    {
        public static event Action OnMeasurementChanged;
        
        public static EMeasure ActiveMeasure
        {
            get
            {
                if (_activeMeasure == EMeasure.None)
                    _activeMeasure = PlayerPrefs.GetInt("measure", 0) == 0 ? EMeasure.Kilometers : EMeasure.Miles;
                return _activeMeasure;
            }
            set
            {
                if (_activeMeasure == EMeasure.None)
                    _activeMeasure = PlayerPrefs.GetInt("measure", 0) == 0 ? EMeasure.Kilometers : EMeasure.Miles;

                if (_activeMeasure == value)
                    return;
                
                _activeMeasure = value;
                PlayerPrefs.SetInt("measure", _activeMeasure == EMeasure.Kilometers ? 0 : 1);
                OnMeasurementChanged?.Invoke();
            }
        }

        private static EMeasure _activeMeasure;
        private const float KmToMilesFactor = 0.621371f;
        
        public static float UnitToMeasure(float maxSpeed, float unit)
        {
            var t = unit / maxSpeed;
            var km = Mathf.Lerp(0f, 923.4f, t);
            if (ActiveMeasure == EMeasure.Kilometers)
                return km;
            
            return km * KmToMilesFactor;
        }
        
        public static float UnitToMeasureDistance(float maxSpeed, float unit)
        {
            var t = unit / maxSpeed;
            var km = Mathf.LerpUnclamped(0f, 923.4f, t) / 3600;
            if (ActiveMeasure == EMeasure.Kilometers)
                return km;
            
            return km * KmToMilesFactor;
        }
    }
}