using UnityEngine;
using Bodzio2k.BattleSystem;
using Bodzio2k.Unit;

public class OrangeTile : MonoBehaviour
{
    private Unit unit = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Unit>(out unit))
        {
            unit.sm.changeState<EnteringWinnigArea>();
        };
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Unit>(out unit))
        {
            unit.sm.changeState<LeavingWinnigArea>();
        };
    }
}
