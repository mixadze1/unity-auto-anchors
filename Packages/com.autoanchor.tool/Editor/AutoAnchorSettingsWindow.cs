using UnityEditor;
using UnityEngine;

namespace AutoAnchor.Editor
{
    public sealed class AutoAnchorSettingsWindow : EditorWindow
    {
        private const string MenuName = "Tools/AutoAnchor/Settings";

        private AutoAnchorConfig _config;
        private SerializedObject _serializedConfig;

        [MenuItem(MenuName, false, 1100)]
        private static void Open()
        {
            var window = GetWindow<AutoAnchorSettingsWindow>("AutoAnchor Settings");
            window.minSize = new Vector2(360f, 235f);
            window.Show();
        }

        private void OnEnable()
        {
            LoadConfig();
        }

        private void OnGUI()
        {
            if (_config == null || _serializedConfig == null)
            {
                LoadConfig();
            }

            EditorGUILayout.LabelField("AutoAnchor Settings", EditorStyles.boldLabel);
            EditorGUILayout.HelpBox(
                "Shortcuts are active while the Scene view is focused and at least one RectTransform is selected.",
                MessageType.Info);

            _serializedConfig.Update();
            EditorGUILayout.PropertyField(
                _serializedConfig.FindProperty("_anchorToBoundsShortcut"),
                new GUIContent("Border shortcut", "Runs Tools/AutoAnchor/Border. Default: Shift+["),
                true);
            EditorGUILayout.PropertyField(
                _serializedConfig.FindProperty("_anchorToCenterShortcut"),
                new GUIContent("Center shortcut", "Runs Tools/AutoAnchor/Center. Default: Shift+]"),
                true);

            if (_serializedConfig.ApplyModifiedProperties())
            {
                EditorUtility.SetDirty(_config);
            }

            GUILayout.FlexibleSpace();
            using (new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button(new GUIContent("Reset defaults", "Restore Shift+[ for Border and Shift+] for Center.")))
                {
                    Undo.RecordObject(_config, "Reset AutoAnchor Shortcuts");
                    _config.ResetShortcuts();
                    EditorUtility.SetDirty(_config);
                    _serializedConfig.Update();
                }

                if (GUILayout.Button(new GUIContent("Select asset", "Select the AutoAnchorConfig ScriptableObject in the Project window.")))
                {
                    Selection.activeObject = _config;
                    EditorGUIUtility.PingObject(_config);
                }
            }
        }

        private void LoadConfig()
        {
            _config = AutoAnchorConfigResolver.GetOrCreateConfig();
            _serializedConfig = new SerializedObject(_config);
        }
    }
}
