using UnityEngine;
using Bodzio2k.Unit;

public class WhiteTile : MonoBehaviour
{
    private Unit unit = null;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Unit>(out unit))
        {
            Debug.LogFormat("{0} entered reduced damage area...", collision.gameObject.name);

            unit.sm.changeState<ChangeDamageMultiplier>();
        };
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Unit>(out unit))
        {
            Debug.LogFormat("{0} leaved reduced damage area...", collision.gameObject.name);

            unit.sm.changeState<ChangeDamageMultiplier>();
        };
    }
}
