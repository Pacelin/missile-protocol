using UnityEditor;
using UnityEditor.Callbacks;

namespace Plugins.CICD
{
    public static class JamBuildUploadSettings
    {
        public static string UserName
        {
            get => EditorPrefs.GetString("itch_user", "pacelin");
            set => EditorPrefs.SetString("itch_user", value);
        }
        
        public static string GameName
        {
            get => EditorPrefs.GetString("itch_game", "ludum_dare_");
            set => EditorPrefs.SetString("itch_game", value);
        }

        public static string Version
        {
            get => EditorPrefs.GetString("itch_game_version", "1.0.0");
            set => EditorPrefs.SetString("itch_game_version", value);
        }

        public static string Platform
        {
            get => EditorPrefs.GetString("itch_platform", "HTML5");
            set => EditorPrefs.SetString("itch_platform", value);
        }

        public static string LastBuildPath
        {
            get => EditorPrefs.GetString("last_build_path");
            set => EditorPrefs.SetString("last_build_path", value);
        }

        [PostProcessBuild(1)]
        public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject) =>
            LastBuildPath = pathToBuiltProject;
    }
}