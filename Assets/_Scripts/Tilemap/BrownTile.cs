using UnityEngine;
using Bodzio2k.Unit;
using Bodzio2k.BattleSystem;

namespace Bodzio2k.Tiles
{
    public class BrownTile : MonoBehaviour
    {
        private Unit.Unit unit = null;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out unit))
            {
                Debug.LogFormat("{0} entered slowing area...", collision.gameObject.name);

                unit.actionsRemaining = 0;
            };
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out unit))
            {
                Debug.LogFormat("{0} leaved slowing area...", collision.gameObject.name);
            };
        }
    }
}