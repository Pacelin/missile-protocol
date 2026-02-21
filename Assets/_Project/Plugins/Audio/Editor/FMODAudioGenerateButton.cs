using UnityEditor.Toolbars;

namespace Plugins.Audio.Editor
{
    public class FMODAudioGenerateButton
    {
        [MainToolbarElement("Tools/Refresh FMOD", defaultDockPosition = MainToolbarDockPosition.Right)]
        public static MainToolbarElement RegenerateFMOD()
        {
            var icon = FMODUtilsInternal.GetFMODStudioIcon();
            var content = new MainToolbarContent("Refresh FMOD", icon, string.Empty);
            return new MainToolbarButton(content, () => { FMODAudioGenerator.Generate(); });
        }
    }
}