using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class ShowingContextMenu : SKState<BattleSystem>
{
    private Unit unit;
    
    public override void update(float deltaTime)
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            unit.HideContextMenu();

            _context.sm.changeState<Idle>();
        }
    }

    public override void begin()
    {
        base.begin();

        unit = _context.selectedPawn.GetComponent<Unit>();

        unit.ShowContextMenu();

        ContextMenuHandler.OnMove += ContextMenuHandler_OnMove; 
    }

    private void ContextMenuHandler_OnMove(object sender, System.EventArgs e)
    {
        //unit.HideContextMenu();

        _context.sm.changeState<MovingPawn>();
    }
}
