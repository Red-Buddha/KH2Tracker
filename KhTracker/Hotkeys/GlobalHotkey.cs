using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

/*
    Credits to Kettlesimulator's YouTube video for the simple solution - https://www.youtube.com/watch?v=qLxqoh1JLnM
*/

namespace KhTracker.Hotkeys
{
    public class GlobalHotkey
    {
        public ModifierKeys Modifier { get; set; }

        public Key Key { get; set; }

        public Action Callback { get; set; }

        public bool CanExecute { get; set; }

        public GlobalHotkey(ModifierKeys modifier, Key key, Action callback, bool canExecute = true)
        {
            Modifier = modifier;
            Key = key;
            Callback = callback;
            CanExecute = canExecute;
        }
    }
}
