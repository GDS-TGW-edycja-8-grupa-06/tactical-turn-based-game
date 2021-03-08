using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContextMenuHandler : MonoBehaviour
{
    public static event EventHandler OnMove;
    public static event EventHandler OnAttack;

    [SerializeField]
    public Button buttonMove;

    [SerializeField]
    public Button buttonAttack;

    public void OnMoveButtonClicked()
    {
        OnMove?.Invoke(this, EventArgs.Empty);
    }

    public void OnAttackButtonClicked()
    {
        OnAttack?.Invoke(this, EventArgs.Empty);
    }
}
