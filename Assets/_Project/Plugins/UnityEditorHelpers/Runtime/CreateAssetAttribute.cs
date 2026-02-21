using System;
using System.Diagnostics;
using JetBrains.Annotations;

namespace Plugins.UnityEditorHelpers
{
    [AttributeUsage(AttributeTargets.Class)]
    [MeansImplicitUse]
    [Conditional("UNITY_EDITOR")]
    [PublicAPI]
    public class CreateAssetAttribute : Attribute
    {
        public string Name { get; }
        
        public CreateAssetAttribute(string name) => Name = name;
    }
}