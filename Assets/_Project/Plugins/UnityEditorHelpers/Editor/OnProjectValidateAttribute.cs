using System;
using JetBrains.Annotations;

namespace Plugins.UnityEditorHelpers.Editor
{
    [AttributeUsage(AttributeTargets.Method)]
    [MeansImplicitUse]
    [PublicAPI]
    public class OnProjectValidateAttribute : Attribute { }
}