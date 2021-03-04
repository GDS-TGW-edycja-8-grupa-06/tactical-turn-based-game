using UnityEngine;
using Bodzio2k.Unit;

public class WhiteTile : MonoBehaviour
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
            unit.sm.changeState<ChangeDamageMultiplier>();
        };
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Unit>(out unit))
        {
            unit.sm.changeState<ChangeDamageMultiplier>();
        };
    }
}
