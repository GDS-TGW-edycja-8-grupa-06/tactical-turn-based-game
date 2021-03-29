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

        public void OnSwitchModeClicked()
        {
            OnSwitchMode?.Invoke(this, EventArgs.Empty);
        }

        public void OnNewGameClicked()
        {
            OnNewGame?.Invoke(this, EventArgs.Empty);
        }
    }
}
