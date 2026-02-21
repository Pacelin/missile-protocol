using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using UnityEditor;
using Debug = UnityEngine.Debug;

namespace Plugins.CICD
{
    public static class JamUploader
    {
        private const string BUTLER_PATH = @"c:\itch-butler\butler.exe";
        private const string UPGRADE_ARGS = "upgrade --assume-yes";
        private const string LOGIN_ARGS = "login";
        private const string PUSH_ARGS = "push \"{0}\" {1}/{2}:{3} --userversion {4}";

        private const string JAM_UPLOADER = "Jam Uploader";
        
        public static void RunUpload()
        {
            try
            {
                if (!Directory.Exists(JamBuildUploadSettings.LastBuildPath))
                {
                    Debug.LogError("Last build directory not found.");
                    return;
                }

                var directory = JamBuildUploadSettings.LastBuildPath;
                var user = JamBuildUploadSettings.UserName.ToLower();
                var game = JamBuildUploadSettings.GameName.ToLower();
                var version = JamBuildUploadSettings.Version;
                var channel = JamBuildUploadSettings.Platform;
                
                EditorUtility.DisplayProgressBar(JAM_UPLOADER, "Check for updates", 0.1f);
                Thread.Sleep(500);
                CheckForUpdates();
                
                EditorUtility.DisplayProgressBar(JAM_UPLOADER, "Login", 0.2f);
                Thread.Sleep(500);
                Login();
                
                EditorUtility.DisplayProgressBar(JAM_UPLOADER, "Upload", 0.5f);
                Thread.Sleep(500);
                Upload(directory, user, game, channel, version);
                
                EditorUtility.DisplayProgressBar(JAM_UPLOADER, "Uploaded success", 1f);
                Thread.Sleep(500);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            finally
            {
                EditorUtility.ClearProgressBar();
            }
        }

        private static void CheckForUpdates()
        {
            var startInfo = new ProcessStartInfo(BUTLER_PATH, UPGRADE_ARGS);
            startInfo.UseShellExecute = false;
            var process = new Process();
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
        }

        private static void Login()
        {
            var startInfo = new ProcessStartInfo(BUTLER_PATH, LOGIN_ARGS);
            startInfo.UseShellExecute = false;
            var process = new Process();
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
        }

        private static void Upload(string directory, string user, string game, string channel, string version)
        {
            var arguments = string.Format(PUSH_ARGS, directory, user, game, channel, version);
            var startInfo = new ProcessStartInfo(BUTLER_PATH, arguments);
            
            startInfo.UseShellExecute = false;
            var process = new Process();
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
        }
    }
}