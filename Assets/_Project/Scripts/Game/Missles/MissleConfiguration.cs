using UnityEngine;

namespace Project.Game.Missles
{
    [CreateAssetMenu(menuName = "Game/Missle")]
    public class MissleConfiguration : ScriptableObject
    {
        public int MissleIndex => _missleIndex;
        public int MarkIndex => _markIndex;
        public string Series => _series;
        public string Length => _length;
        public string SolveCode => _solveCode;
        
        [SerializeField] private int _missleIndex;
        [Header("● - 0; ⭐ - 1; ⬛ - 2; ▲ - 3")]
        [SerializeField] private int _markIndex;
        [SerializeField] private string _series;
        [SerializeField] private string _length;
        [Space]
        [SerializeField] private string _solveCode = "12345678LT";
    }
}