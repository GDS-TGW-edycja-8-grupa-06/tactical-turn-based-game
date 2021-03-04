using UnityEngine;
using Bodzio2k.Unit;

public class BlueTile : MonoBehaviour
{
    private Unit unit = null;

    private void Start()
    {
        return;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Unit>(out unit))
        {
            //unit.battleSystem.sm.changeState<BattleSystemEnteringWinnigArea>();
            unit.sm.changeState<Idle>();
        };
    }
}
