using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Plugins.UnityEditorHelpers.Editor
{
    public static class CreateAssetAttributeProcessor
    {
        [OnProjectValidate]
        private static void Process()
        {
            var types = TypeCache.GetTypesWithAttribute<CreateAssetAttribute>();
            var soType = typeof(ScriptableObject);
            foreach (var type in types)
            {
                if (!soType.IsAssignableFrom(type))
                {
                    Debug.LogError($"Type {type} is not ScriptableObject");
                    continue;
                }
                
                var attribute = type.GetCustomAttribute<CreateAssetAttribute>();
                AssetsHelper.ValidateAssetExists(type, "Assets/_Project/Settings/" + attribute.Name + ".asset");
            }
        }
        
        [OnProjectValidate]
        private static void ProcessResources()
        {
            var types = TypeCache.GetTypesWithAttribute<CreateResourceAssetAttribute>();
            var soType = typeof(ScriptableObject);
            foreach (var type in types)
            {
                if (!soType.IsAssignableFrom(type))
                {
                    Debug.LogError($"Type {type} is not ScriptableObject");
                    continue;
                }
                
                var attribute = type.GetCustomAttribute<CreateResourceAssetAttribute>();
                AssetsHelper.ValidateAssetExists(type, "Assets/Resources/" + attribute.Name + ".asset");
            }
        }
    }
}