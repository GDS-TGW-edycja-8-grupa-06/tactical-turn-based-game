using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContextMenuHandler : MonoBehaviour
{
    public static event EventHandler OnMove;

    [SerializeField]
    public Button buttonMove;

    [SerializeField]
    public Button buttonAttack;

    private void Start()
    {
        //    GameObject parentPawn = transform.parent.gameObject;
        //    Unit unit = parentPawn.GetComponent<Unit>();

        //    switch (unit.unitAction)
        //    {
        //        case UnitAction.Both:
        //            buttonMove.gameObject.SetActive(false);
        //            buttonAttack.gameObject.SetActive(false);
        //            break;
        //        case UnitAction.Attack:
        //            buttonAttack.gameObject.SetActive(false);
        //            break;
        //        case UnitAction.Move:
        //            buttonMove.gameObject.SetActive(false);
        //            break;
        //    }
    }

    public void OnMoveButtonClicked()
    {
        OnMove?.Invoke(this, EventArgs.Empty);
    }
    
}
