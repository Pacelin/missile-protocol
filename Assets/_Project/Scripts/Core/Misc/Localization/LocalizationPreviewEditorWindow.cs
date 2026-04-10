#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Localization;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.Localization;

namespace Project.Core.Misc.Localization
{
    public class LocalizationPreviewEditorWindow : EditorWindow
    {
        private static int ActiveLocaleIndex
        {
            get => EditorPrefs.GetInt("preview_locale", 0);
            set => EditorPrefs.SetInt("preview_locale", value);
        }
        
        private static Locale ActiveLocale
        {
            get
            {
                var locale = LocalizationEditorSettings.GetLocales()[ActiveLocaleIndex];
                return locale;
            }
        }
        
        public static string GetLocalizedString(LocalizedString str)
        {
            var table = LocalizationEditorSettings.ActiveLocalizationSettings.GetStringDatabase()
                .GetTable(str.TableReference, ActiveLocale);
            return table[str.TableEntryReference.KeyId].Value;
        }
        
        [MainToolbarElement("Tools/Localization Preview", defaultDockPosition = MainToolbarDockPosition.Right)]
        public static MainToolbarElement LocalizationPreviewToolbarElement()
        {
            var activeLocaleName = ActiveLocale.LocaleName;
            var dropDown = new MainToolbarDropdown(new MainToolbarContent(activeLocaleName), rect =>
            {
                var menu = new GenericMenu();
                var locales = LocalizationEditorSettings.GetLocales();
                for (int i = 0; i < locales.Count; i++)
                {
                    var index = i;
                    var localeName = locales[i].LocaleName;
                    menu.AddItem(new GUIContent(localeName), localeName == activeLocaleName, () =>
                    {
                        ActiveLocaleIndex = index;
                        UpdateObjects();
                        MainToolbar.Refresh("Tools/Localization Preview");
                    });
                }
                menu.DropDown(rect);
            });
            return dropDown;
        }

        private static void UpdateObjects()
        {
            var objects = FindObjectsByType<LocalizeText>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            foreach (var obj in objects)
                obj.OnValidate();
        }
    }
}
#endif