using UnityEngine;

namespace Project.Game.Map
{
    public interface IMapUnit
    {
        Vector2 Position { get; }
        float Rotation { get; }
    }
}