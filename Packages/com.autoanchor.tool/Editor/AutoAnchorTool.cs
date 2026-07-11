using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace AutoAnchor.Editor
{
    [InitializeOnLoad]
    public static class AutoAnchorTool
    {
        private const string MenuRoot = "Tools/Auto Anchor/";
        private const string AnchorToBoundsUndoName = "Anchor to Bounds";
        private const string AnchorToCenterUndoName = "Anchor to Center";

        static AutoAnchorTool()
        {
            SceneView.duringSceneGui += HandleSceneViewShortcut;
        }

        [MenuItem(MenuRoot + "Anchor to Bounds", false, 1000)]
        public static void AnchorToBounds()
        {
            ApplyToSelection(AnchorMode.Bounds);
        }

        [MenuItem(MenuRoot + "Anchor to Bounds", true)]
        private static bool CanAnchorToBounds()
        {
            return HasRectTransformSelection();
        }

        [MenuItem(MenuRoot + "Anchor to Center", false, 1001)]
        public static void AnchorToCenter()
        {
            ApplyToSelection(AnchorMode.Center);
        }

        [MenuItem(MenuRoot + "Anchor to Center", true)]
        private static bool CanAnchorToCenter()
        {
            return HasRectTransformSelection();
        }

        [MenuItem(MenuRoot + "Create Configuration", false, 1100)]
        private static void CreateConfiguration()
        {
            var existingConfig = AutoAnchorConfigResolver.FindConfig();
            if (existingConfig != null)
            {
                Selection.activeObject = existingConfig;
                EditorGUIUtility.PingObject(existingConfig);
                return;
            }

            var config = ScriptableObject.CreateInstance<AutoAnchorConfig>();
            var path = AssetDatabase.GenerateUniqueAssetPath("Assets/AutoAnchorConfig.asset");
            AssetDatabase.CreateAsset(config, path);
            AssetDatabase.SaveAssets();
            Selection.activeObject = config;
            EditorGUIUtility.PingObject(config);
        }

        private static void HandleSceneViewShortcut(SceneView sceneView)
        {
            var currentEvent = Event.current;
            if (currentEvent == null || currentEvent.type != EventType.KeyDown || !HasRectTransformSelection())
            {
                return;
            }

            var config = AutoAnchorConfigResolver.FindConfigOrDefault();
            if (Matches(currentEvent, config.AnchorToBoundsShortcut))
            {
                AnchorToBounds();
                currentEvent.Use();
            }
            else if (Matches(currentEvent, config.AnchorToCenterShortcut))
            {
                AnchorToCenter();
                currentEvent.Use();
            }
        }

        private static bool Matches(Event currentEvent, AutoAnchorShortcut shortcut)
        {
            return shortcut != null && shortcut.Matches(
                currentEvent.keyCode,
                currentEvent.shift,
                currentEvent.control,
                currentEvent.alt,
                currentEvent.command);
        }

        private static void ApplyToSelection(AnchorMode mode)
        {
            var rectTransforms = Selection.transforms.OfType<RectTransform>().ToArray();
            if (rectTransforms.Length == 0)
            {
                Debug.LogWarning("Auto Anchor: select at least one RectTransform.");
                return;
            }

            var appliedCount = rectTransforms.Count(rectTransform => Apply(rectTransform, mode));
            Debug.Log($"Auto Anchor: {mode} applied to {appliedCount} RectTransform(s).");
        }

        private static bool Apply(RectTransform rectTransform, AnchorMode mode)
        {
            var parent = rectTransform.parent as RectTransform;
            if (parent == null)
            {
                Debug.LogError($"Auto Anchor failed: '{rectTransform.name}' has no RectTransform parent.");
                return false;
            }

            var parentRect = parent.rect;
            if (Mathf.Approximately(parentRect.width, 0f) || Mathf.Approximately(parentRect.height, 0f))
            {
                Debug.LogError($"Auto Anchor failed: '{parent.name}' parent size is zero.");
                return false;
            }

            GetBoundsInParentSpace(rectTransform, parent, out var min, out var max);
            var anchorMin = ToNormalizedAnchor(min, parentRect);
            var anchorMax = ToNormalizedAnchor(max, parentRect);

            if (mode == AnchorMode.Center)
            {
                var center = parent.InverseTransformPoint(rectTransform.TransformPoint(rectTransform.rect.center));
                var centerAnchor = ToNormalizedAnchor(center, parentRect);
                anchorMin = centerAnchor;
                anchorMax = centerAnchor;
            }

            var offsetMin = min - AnchorToLocalPoint(parentRect, anchorMin);
            var offsetMax = max - AnchorToLocalPoint(parentRect, anchorMax);

            Undo.RecordObject(rectTransform, mode == AnchorMode.Bounds ? AnchorToBoundsUndoName : AnchorToCenterUndoName);
            rectTransform.anchorMin = anchorMin;
            rectTransform.anchorMax = anchorMax;
            rectTransform.offsetMin = offsetMin;
            rectTransform.offsetMax = offsetMax;
            EditorUtility.SetDirty(rectTransform);
            PrefabUtility.RecordPrefabInstancePropertyModifications(rectTransform);
            return true;
        }

        private static bool HasRectTransformSelection()
        {
            return Selection.transforms.Any(transform => transform is RectTransform);
        }

        private static void GetBoundsInParentSpace(RectTransform rectTransform, RectTransform parent, out Vector2 min, out Vector2 max)
        {
            var corners = new Vector3[4];
            rectTransform.GetWorldCorners(corners);

            var firstPoint = parent.InverseTransformPoint(corners[0]);
            min = firstPoint;
            max = firstPoint;
            for (var index = 1; index < corners.Length; index++)
            {
                var point = parent.InverseTransformPoint(corners[index]);
                min = Vector2.Min(min, point);
                max = Vector2.Max(max, point);
            }
        }

        private static Vector2 ToNormalizedAnchor(Vector2 localPoint, Rect parentRect)
        {
            return new Vector2(
                Mathf.Clamp01((localPoint.x - parentRect.xMin) / parentRect.width),
                Mathf.Clamp01((localPoint.y - parentRect.yMin) / parentRect.height));
        }

        private static Vector2 AnchorToLocalPoint(Rect parentRect, Vector2 anchor)
        {
            return new Vector2(
                Mathf.Lerp(parentRect.xMin, parentRect.xMax, anchor.x),
                Mathf.Lerp(parentRect.yMin, parentRect.yMax, anchor.y));
        }

        private enum AnchorMode
        {
            Bounds,
            Center
        }
    }

    internal static class AutoAnchorConfigResolver
    {
        private static AutoAnchorConfig _defaultConfig;

        internal static AutoAnchorConfig FindConfigOrDefault()
        {
            return FindConfig() ?? GetDefaultConfig();
        }

        internal static AutoAnchorConfig FindConfig()
        {
            var configGuids = AssetDatabase.FindAssets("t:AutoAnchorConfig");
            if (configGuids.Length == 0)
            {
                return null;
            }

            var configPath = AssetDatabase.GUIDToAssetPath(configGuids.OrderBy(guid => guid, StringComparer.Ordinal).First());
            return AssetDatabase.LoadAssetAtPath<AutoAnchorConfig>(configPath);
        }

        private static AutoAnchorConfig GetDefaultConfig()
        {
            if (_defaultConfig != null)
            {
                return _defaultConfig;
            }

            _defaultConfig = ScriptableObject.CreateInstance<AutoAnchorConfig>();
            _defaultConfig.hideFlags = HideFlags.HideAndDontSave;
            return _defaultConfig;
        }
    }
}
