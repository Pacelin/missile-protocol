using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Project.Game.Missles.Editor
{
    using UnityEngine;
using UnityEditor;

    public class MissleConfigurationCreator : EditorWindow
    {
        private string _text;
        private Vector2 _scrollPosition;
        
        [MenuItem("Tools/Missles Creator")]
        public static void Open()
        {
            var window = GetWindow<MissleConfigurationCreator>();
            window.Show();
        }

        private void OnGUI()
        {
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);
            _text = EditorGUILayout.TextArea(_text);
            if (GUILayout.Button("Create"))
            {
                var path = EditorUtility.SaveFolderPanel("Save", "Assets/", "d");
                if (path != null)
                {
                    path = "Assets/" + path.Split("Assets/")[1];
                    Save(path);
                    Close();
                }
            }
            EditorGUILayout.EndScrollView();
        }

        private void Save(string path)
        {
            var lines = _text.Split('\n')
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .ToArray();
            for (int i = 0; i < lines.Length; i++)
            {
                var fileName = "SO_Missle_" + (i + 1) + ".asset";
                var so = ScriptableObject.CreateInstance<MissleConfiguration>();
                var serialized = new SerializedObject(so);
                
                var missleIndexProp = serialized.FindProperty("_missleIndex");
                var markIndexProp = serialized.FindProperty("_markIndex");
                var seriesProp = serialized.FindProperty("_series");
                var lengthProp = serialized.FindProperty("_length");
                var solveCodeProp = serialized.FindProperty("_solveCode");

                var splitLine = lines[i].Split(' ');
                missleIndexProp.intValue = int.Parse(splitLine[0]);
                markIndexProp.intValue = int.Parse(splitLine[1]);
                seriesProp.stringValue = splitLine[2];
                lengthProp.stringValue = splitLine[3] + "m";
                solveCodeProp.stringValue = splitLine[4];

                serialized.ApplyModifiedPropertiesWithoutUndo();
                AssetDatabase.CreateAsset(so, path + "/" + fileName);
                serialized.Dispose();
            }
            
            AssetDatabase.SaveAssets();
        }
    }
}