using UnityEngine;
using System.Collections;
using Prime31.StateKit;
using UnityEngine.Tilemaps;

public class MovingPawn : SKState<BattleSystem>
{
    public override void update(float deltaTime)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 targetPosition = Vector3.zero;

            if (IsMoveValid(ref targetPosition))
            {
                if (IsMoveWithinRange(targetPosition))
                {                    
                    _context.selectedPawn.transform.position = targetPosition;
                }
                else
                {
                    _machine.changeState<Idle>();
                }

                _machine.changeState<ChangeSide>();                
            }

            _context.sm.changeState<Idle>();
        }
    }

    private bool IsMoveValid(ref Vector3 targetPosition)
    {
        bool moveIsValid = false;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);

        Vector3Int tilemapPosition = _context.grid.WorldToCell(mousePosition);
        
        Tile tile = _context.grid.GetTile<Tile>(tilemapPosition);
        LayerMask restrictedAreaMask = _context.restrictedArea;
        
        RaycastHit2D hit = Physics2D.Raycast(mousePosition2D, Vector2.zero, Mathf.Infinity, restrictedAreaMask);
        
        if (hit.collider == null)
        {
            targetPosition = new Vector3(tilemapPosition.x + 1, tilemapPosition.y, 0);

            moveIsValid = true;
        }
        else
        {
            moveIsValid = false;
        }

        return moveIsValid;
    }

    private bool IsMoveWithinRange(Vector3 targetPosition)
    {
        Unit unit = _context.selectedPawn.GetComponent<Unit>();
        UnitProperties unitProperties = unit.unitProperties;
        int moveRange = unitProperties.moveRange;
        
        //if (Mathf.Floor(targetPosition.x)

        return true;
    }
}