using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextMenuHandler : MonoBehaviour
{
    public static event EventHandler OnMove;

    public void OnMoveButtonClicked()
    {
        OnMove?.Invoke(this, EventArgs.Empty);
    }
    
}
