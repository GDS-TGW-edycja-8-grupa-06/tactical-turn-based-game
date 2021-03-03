using UnityEngine;
using Prime31.StateKit;

public class BattleSystemIdle : SKState<BattleSystem>
{
    private LayerMask playerLayerMask;
 
    public override void update(float deltaTime)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);

            _context.cameraFocus.transform.position = mousePosition;

            RaycastHit2D hit = Physics2D.Raycast(mousePosition2D, Vector2.zero, Mathf.Infinity, playerLayerMask);

            if (hit.collider != null)
            {   
                _context.selectedUnit = hit.collider.gameObject;

                _context.selectedUnit.GetComponent<Unit>().sm.changeState<ShowingContextMenu>();
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
