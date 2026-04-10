using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Core.Misc
{
    public class SetCanvasCamera : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;

        private void OnValidate()
        {
            if (!_canvas)
                _canvas = GetComponent<Canvas>();
        }
        
        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            UpdateCamera();
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        
        private void UpdateCamera()
        {
            if (_canvas.worldCamera)
                return;
            _canvas.worldCamera = Camera.main;
        }
        
        private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1) => UpdateCamera();
    }
}