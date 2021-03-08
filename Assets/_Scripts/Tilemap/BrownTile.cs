using UnityEngine;
using Bodzio2k.Unit;

namespace Bodzio2k
{
    public class BrownTile : MonoBehaviour
    {
        private Unit.Unit unit = null;

        private void Start()
        {
            return;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out unit))
            {
                unit.sm.changeState<DisableAttack>();
            };
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out unit))
            {
                unit.sm.changeState<DisableAttack>();
            };
        }
    }
}