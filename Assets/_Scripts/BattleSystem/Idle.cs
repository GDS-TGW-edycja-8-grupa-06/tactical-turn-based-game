using UnityEngine;
using Prime31.StateKit;
using Bodzio2k.Unit;

namespace Bodzio2k.BattleSystem
{
    public class Idle : SKState<BattleSystem>
    {
        private LayerMask playerLayerMask;
        
        public override void update(float deltaTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);

                //_context.cameraFocus.transform.position = mousePosition;

                RaycastHit2D hit = Physics2D.Raycast(mousePosition2D, Vector2.zero, Mathf.Infinity, playerLayerMask);

                if (hit.collider != null)
                {
                    Unit.Unit unit;

                    _context.selectedUnit = hit.collider.gameObject;

                    if (_context.touchedUnit != null && _context.touchedUnit != _context.selectedUnit)
                    {
                        _context.cameraPosition = mousePosition;

                        return;
                    }

                    unit = _context.selectedUnit.GetComponent<Unit.Unit>();
                    unit.sm.changeState<ShowingContextMenu>();
                }
                else
                {
                    _context.cameraPosition = mousePosition;
                }
            }
        }

        public override void begin()
        {
            base.begin();

            playerLayerMask = _context.gamePhase == GamePhase.PlayerOne ? _context.playerOneLayerMask : _context.playerTwoLayerMask;

            return;
        }
    }
}