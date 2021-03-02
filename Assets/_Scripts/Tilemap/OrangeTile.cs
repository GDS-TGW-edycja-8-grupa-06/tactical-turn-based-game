using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeTile : MonoBehaviour
{
    private Unit unit = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Unit>(out unit))
        {
            unit.battleSystem.sm.changeState<BattleSystemEnteringWinnigArea>();
        };
    }
}
