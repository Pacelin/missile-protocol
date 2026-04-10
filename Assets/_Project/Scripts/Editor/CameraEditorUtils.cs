using System.IO;
using UnityEditor;
using UnityEngine;

namespace Project.Editor
{
    public static class CameraEditorUtils
    {
        [MenuItem("Tools/Capture Camera")]
        private static void CaptureCamera()
        {
            var resultPath = EditorUtility.SaveFilePanel("Save Capture", Application.persistentDataPath, "capture_0.png",
                ".png");
            if (string.IsNullOrEmpty(resultPath))
                return;
                
            var mainCamera = Camera.main;
            var activeRt = RenderTexture.active;

            var tempRt = RenderTexture.GetTemporary(Screen.width, Screen.height, 32);
            var oldTexture = mainCamera!.targetTexture;
            mainCamera.targetTexture = tempRt;
            RenderTexture.active = tempRt;

            mainCamera.Render();

            var texture2D = new Texture2D(tempRt.width, tempRt.height);
            texture2D.ReadPixels(new Rect(0, 0, tempRt.width, tempRt.height), 0, 0);
            texture2D.Apply();
            
            RenderTexture.active = activeRt;
            mainCamera.targetTexture = oldTexture;

            var pngBytes = texture2D.EncodeToPNG();

            var directoryPath = Path.GetDirectoryName(resultPath);
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath!);
            using (var fileStream = File.Create(resultPath))
            {
                fileStream
                    .WriteAsync(pngBytes, 0, pngBytes.Length)
                    .Wait();
            }

            RenderTexture.ReleaseTemporary(tempRt);
        }
    }
}