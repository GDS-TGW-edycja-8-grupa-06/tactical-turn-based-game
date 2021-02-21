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
            Vector3Int tilemapPos = _context.walkableArea[0].WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            Tile tile = _context.walkableArea[0].GetTile<Tile>(tilemapPos);

            _context.sm.changeState<Idle>();

            return;
        }
    }

    public override void begin()
    {
        return;
    }
}