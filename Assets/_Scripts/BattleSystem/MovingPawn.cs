using UnityEngine;
using System.Collections;
using Prime31.StateKit;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class MovingPawn : SKState<BattleSystem>
{
    Unit unit;

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

                    unit.HideContextMenu();

                    if (unit.unitAction == UnitAction.Both)
                    {
                        unit.unitAction = UnitAction.Attack;
                    }
                }
                else
                {
                    return;
                }

                
                _machine.changeState<ChangeSide>();                
            }
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
        List<Vector3> availablePostions = GetAvailablePositions(_context.selectedPawn);

        if (availablePostions.Contains(targetPosition))
        {
            return true;
        }
        
        return false;
    }

    private List<Vector3> GetAvailablePositions(GameObject pawn)
    {
        List<Vector3> availablePostions = new List<Vector3>();
        Vector3 pawnPostion = pawn.transform.position;
        UnitProperties unitProperties = unit.unitProperties;
        int moveRange = unitProperties.moveRange;

        availablePostions.Add(new Vector3(pawnPostion.x + moveRange, pawnPostion.y));
        availablePostions.Add(new Vector3(pawnPostion.x + -moveRange, pawnPostion.y));
        availablePostions.Add(new Vector3(pawnPostion.x, pawnPostion.y + moveRange));
        availablePostions.Add(new Vector3(pawnPostion.x, pawnPostion.y + -moveRange));
        

        return availablePostions;
    }

    public override void begin()
    {
        base.begin();

        unit = _context.selectedPawn.GetComponent<Unit>();
    }
}