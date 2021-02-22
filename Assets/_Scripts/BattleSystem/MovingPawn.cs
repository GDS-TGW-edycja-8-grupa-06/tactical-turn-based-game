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
            Vector3Int tilemapPosition = _context.walkableArea[0].WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            Tile tile = _context.walkableArea[0].GetTile<Tile>(tilemapPosition);

            _context.sm.changeState<Idle>();

            _context.selectedUnit.transform.position = new Vector3(tilemapPosition.x + 1, tilemapPosition.y, 0);

            return;
        }
    }

    public override void begin()
    {
        return;
    }
}