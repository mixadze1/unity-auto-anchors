using System;
using UnityEngine;

namespace AutoAnchor
{
    [CreateAssetMenu(fileName = "AutoAnchorConfig", menuName = "Auto Anchor/Configuration")]
    public sealed class AutoAnchorConfig : ScriptableObject
    {
        [Header("Scene View shortcuts")]
        [Tooltip("Runs Tools/AutoAnchor/Border. Default: Shift+[")]
        [SerializeField] private AutoAnchorShortcut _anchorToBoundsShortcut = new AutoAnchorShortcut(KeyCode.LeftBracket, true);

        [Tooltip("Runs Tools/AutoAnchor/Center. Default: Shift+]")]
        [SerializeField] private AutoAnchorShortcut _anchorToCenterShortcut = new AutoAnchorShortcut(KeyCode.RightBracket, true);

        public AutoAnchorShortcut AnchorToBoundsShortcut => _anchorToBoundsShortcut;
        public AutoAnchorShortcut AnchorToCenterShortcut => _anchorToCenterShortcut;

        public void ResetShortcuts()
        {
            _anchorToBoundsShortcut = new AutoAnchorShortcut(KeyCode.LeftBracket, true);
            _anchorToCenterShortcut = new AutoAnchorShortcut(KeyCode.RightBracket, true);
        }

        private void Reset()
        {
            ResetShortcuts();
        }
    }

    [Serializable]
    public sealed class AutoAnchorShortcut
    {
        [Tooltip("The non-modifier key in the shortcut.")]
        [SerializeField] private KeyCode _key;

        [Tooltip("Requires Shift to be held.")]
        [SerializeField] private bool _shift;

        [Tooltip("Requires Control to be held.")]
        [SerializeField] private bool _control;

        [Tooltip("Requires Alt to be held.")]
        [SerializeField] private bool _alt;

        [Tooltip("Requires Command to be held on macOS.")]
        [SerializeField] private bool _command;

        public KeyCode Key => _key;
        public bool Shift => _shift;
        public bool Control => _control;
        public bool Alt => _alt;
        public bool Command => _command;

        public AutoAnchorShortcut(KeyCode key, bool shift = false, bool control = false, bool alt = false, bool command = false)
        {
            _key = key;
            _shift = shift;
            _control = control;
            _alt = alt;
            _command = command;
        }

        public bool Matches(KeyCode key, bool shift, bool control, bool alt, bool command)
        {
            return _key == key &&
                   _shift == shift &&
                   _control == control &&
                   _alt == alt &&
                   _command == command;
        }
    }
}
