using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Bodzio2k.UI
{
    public class UIHandler : MonoBehaviour
    {
        public static event EventHandler OnSwitchMode;
        public static event EventHandler OnNewGame;
        public static event EventHandler OnQuit;
        public static event EventHandler OnHowToPlay;

        public void OnSwitchModeClicked()
        {
            OnSwitchMode?.Invoke(this, EventArgs.Empty);
        }

        public void OnNewGameClicked()
        {
            OnNewGame?.Invoke(this, EventArgs.Empty);
        }

        public void OnQuickClicked()
        {
            OnQuit?.Invoke(this, EventArgs.Empty);
        }

        public void OnHowToPlayClicked()
        {
            OnHowToPlay?.Invoke(this, EventArgs.Empty);
        }
    }
}
