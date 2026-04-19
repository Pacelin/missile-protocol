using UnityEngine;

namespace Project.Game.Map
{
    public class MapMissleParticles : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;

        private void OnEnable()
        {
            MapMissle.OnExplode += OnMapMissleExplode;
        }

        private void OnDisable()
        {
            MapMissle.OnExplode -= OnMapMissleExplode;
        }

        private void OnMapMissleExplode(Vector3 position)
        {
            _particleSystem.transform.position = position;
            _particleSystem.Play();
        }
    }
}