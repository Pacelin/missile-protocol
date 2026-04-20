using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Game.Utils
{
    public class SmoothLook : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _target;
        [SerializeField] private Vector2 _xBounds;
        [SerializeField] private Vector2 _yBounds;
        [SerializeField] private bool _invertX;
        [SerializeField] private bool _invertY;
        [SerializeField] private int _smoothFrames;

        private readonly Queue<Vector2> _smoothArray = new();
        
        private void Update()
        {
            Vector2 viewportPosition = (Vector2) _camera.ScreenToViewportPoint(Input.mousePosition);
            var x = viewportPosition.x * 2 - 1f;
            var y = viewportPosition.y * 2 - 1f;

            var tx = x * 0.5f + 0.5f;
            var ty = y * 0.5f + 0.5f;
            
            if (_invertX)
                tx = 1 - tx;
            if (_invertY)
                ty = 1 - ty;

            var resultX = Mathf.Lerp(_xBounds.x, _xBounds.y, tx);
            var resultY = Mathf.Lerp(_yBounds.x, _yBounds.y, ty);

            _smoothArray.Enqueue(new Vector2(resultX, resultY));

            if (_smoothArray.Count > _smoothFrames)
                _smoothArray.Dequeue();

            Vector2 resultOffset = Vector2.zero;
            foreach (var value in _smoothArray)
                resultOffset += value;
            resultOffset /= _smoothArray.Count;
            
            var targetPosition = _target.position;
            targetPosition.x = resultOffset.x;
            targetPosition.y = resultOffset.y;

            _target.position = targetPosition;
        }
    }
}