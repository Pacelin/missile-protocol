using UnityEditor;

namespace Plugins.UnityEditorHelpers.Editor
{
    [InitializeOnLoad]
    public static class OnProjectValidateAttributeProcessor
    {
        static OnProjectValidateAttributeProcessor()
        {
            EditorApplication.delayCall += () =>
            {
                InvokeOnValidate();
                EditorApplication.projectChanged += InvokeOnValidate;
            };
        }
        
        private static void InvokeOnValidate()
        {
            var methods = TypeCache.GetMethodsWithAttribute<OnProjectValidateAttribute>();
            foreach (var method in methods)
            {
                if (!method.IsStatic)
                    continue;
                method.Invoke(null, null);
            }
        }
    }
}