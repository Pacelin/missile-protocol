using System;
using System.Diagnostics;
using JetBrains.Annotations;

namespace Plugins.UnityEditorHelpers
{
    [AttributeUsage(AttributeTargets.Class)]
    [MeansImplicitUse]
    [Conditional("UNITY_EDITOR")]
    [PublicAPI]
    public class CreateResourceAssetAttribute : Attribute
    {
        public string Name { get; }
        
        public CreateResourceAssetAttribute(string name) => Name = name;
    }
}