using System;
using UnityEngine;

namespace AutoAnchor
{
    [CreateAssetMenu(fileName = "AutoAnchorConfig", menuName = "Auto Anchor/Configuration")]
    public sealed class AutoAnchorConfig : ScriptableObject
    {
        [SerializeField] private AutoAnchorShortcut _anchorToBoundsShortcut = new AutoAnchorShortcut(KeyCode.LeftBracket, true);
        [SerializeField] private AutoAnchorShortcut _anchorToCenterShortcut = new AutoAnchorShortcut(KeyCode.RightBracket, true);

        public AutoAnchorShortcut AnchorToBoundsShortcut => _anchorToBoundsShortcut;
        public AutoAnchorShortcut AnchorToCenterShortcut => _anchorToCenterShortcut;

        private void Reset()
        {
            _anchorToBoundsShortcut = new AutoAnchorShortcut(KeyCode.LeftBracket, true);
            _anchorToCenterShortcut = new AutoAnchorShortcut(KeyCode.RightBracket, true);
        }
    }

    [Serializable]
    public sealed class AutoAnchorShortcut
    {
        [SerializeField] private KeyCode _key;
        [SerializeField] private bool _shift;
        [SerializeField] private bool _control;
        [SerializeField] private bool _alt;
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
