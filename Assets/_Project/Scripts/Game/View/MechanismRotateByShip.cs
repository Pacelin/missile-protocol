using UnityEngine;

namespace Project.Game
{
    public class MechanismRotateByShip : MonoBehaviour
    {
        [SerializeField] private Transform _rotateTransform;
        
        private void OnEnable()
        {
            G.ShipModel.OnTransformChanged += OnShipTransformChanged;
            _rotateTransform.rotation = Quaternion.Euler(0, 0, -G.ShipModel.Rotation);
        }

        private void OnDisable()
        {
            G.ShipModel.OnTransformChanged -= OnShipTransformChanged;
        }

        private void OnShipTransformChanged()
        {
            _rotateTransform.rotation = Quaternion.Euler(0, 0, -G.ShipModel.Rotation);
        }
    }
}