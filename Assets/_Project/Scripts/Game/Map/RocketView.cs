using UnityEngine;

namespace Project.Game.Map
{
    public class RocketView : MonoBehaviour
    {
        [SerializeField] private Transform _moveTransform;
        [SerializeField] private Transform _rotateTransform;
        [SerializeField] private Vector3 _offset;
        
        private void OnEnable()
        {
            G.ShipModel.OnTransformChanged += OnShipTransformChanged;
        }

        private void OnDisable()
        {
            G.ShipModel.OnTransformChanged -= OnShipTransformChanged;
        }

        private void OnShipTransformChanged()
        {
            _moveTransform.position = (Vector3) G.ShipModel.Position + _offset;
            _rotateTransform.rotation = Quaternion.Euler(0, 0, G.ShipModel.Rotation);
        }
    }
}