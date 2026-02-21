using System;
using System.IO;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Plugins.UnityEditorHelpers.Editor
{
    [PublicAPI]
    public static class AssetsHelper
    {
        public static void WriteFile(string filePath, string content)
        {
            var directoryPath = Path.GetDirectoryName(filePath);
            ValidateDirectoryExists(directoryPath);
            File.WriteAllText(filePath, content);
        }
        
        public static void ValidateAssetExists(Type assetType, string path)
        {
            if (AssetDatabase.AssetPathExists(path)) 
                return;
            
            var directoryPath = Path.GetDirectoryName(path);
            ValidateDirectoryExists(directoryPath);
            var newAsset = ScriptableObject.CreateInstance(assetType);
            AssetDatabase.CreateAsset(newAsset, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public static void ValidateDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
    }
}