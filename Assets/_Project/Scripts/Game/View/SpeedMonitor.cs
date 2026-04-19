using Project.MainMenu;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

namespace Project.Game
{
    public class SpeedMonitor : MonoBehaviour
    {
        [SerializeField] private TMP_Text _speedText;
        [SerializeField] private TMP_Text _degreesText;
        [SerializeField] private LocalizedString _milesString;
        [SerializeField] private LocalizedString _kmString;
        [SerializeField] private LocalizedString _degreesString;

        private void Update()
        {
            var speed = Measurement.UnitToMeasure(G.ShipModel.LinearVelocity.MaxSpeed,
                G.ShipModel.LinearVelocity.Value.magnitude);
            var localizedString = Measurement.ActiveMeasure == EMeasure.Kilometers ? _kmString : _milesString;
            var degrees = -G.ShipModel.AngularVelocity.Value;

            _speedText.text = speed.ToString("0.0") + " " + localizedString.GetLocalizedString();
            _degreesText.text = degrees.ToString("0.00") + " " + _degreesString.GetLocalizedString();
        }
    }
}