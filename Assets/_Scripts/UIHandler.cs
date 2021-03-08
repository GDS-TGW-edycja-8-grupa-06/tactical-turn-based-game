using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Bodzio2k
{
    public class UIHandler : MonoBehaviour
    {
        public static event EventHandler OnSwitchMode;

        public void OnSwitchModeClicked()
        {
            OnSwitchMode?.Invoke(this, EventArgs.Empty);
        }
    }
}
