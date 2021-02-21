using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Idle : SKState<BattleSystem>
{
    public override void update(float deltaTime)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePosition2D, Vector2.zero);

            if (hit.collider != null)
            {
                _context.selectedUnit = hit.collider.gameObject;
                _context.sm.changeState<MovingPawn>();
            }
        }
    }
}
