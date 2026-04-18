using Unity.Cinemachine;
using UnityEngine;

namespace Project.Game
{
    public class MechanismRotateByFuel : MonoBehaviour
    {
        [SerializeField] private Transform _rotateTransform;
        [SerializeField] private Vector2 _rotationRange;
        [SerializeField] private float _rotateInterpolation = 10f;
        [SerializeField] private NoiseSettings _noiseSettings;
        [SerializeField] private float _noiseStrength;

        private void Update()
        {
            if (G.PauseController.HasAnyPause)
                return;
            
            _noiseSettings.GetSignal(Time.time, out _, out Quaternion rot);
            rot = Quaternion.SlerpUnclamped(Quaternion.identity, rot, _noiseStrength);
            
            var currentRotation = _rotateTransform.rotation;
            var targetZRotation = Mathf.Lerp(_rotationRange.x, _rotationRange.y, G.ShipModel.Fuel.Value);
            var targetRotation = Quaternion.Euler(0, 0, targetZRotation);

            var newRotation = Quaternion.Lerp(currentRotation, targetRotation, _rotateInterpolation * Time.deltaTime);
            newRotation *= rot;
            
            _rotateTransform.rotation = newRotation;
        }
    }
}