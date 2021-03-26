using UnityEngine;
using Bodzio2k.BattleSystem;
using Bodzio2k.Unit;

namespace Bodzio2k.Tiles
{
    public class OrangeTile : MonoBehaviour
    {
        private Unit.Unit unit = null;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent<Unit.Unit>(out unit))
            {
                unit.sm.changeState<EnteringWinnigArea>();
            };
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent<Unit.Unit>(out unit))
            {
                unit.sm.changeState<LeavingWinnigArea>();
            };
        }
    }
}