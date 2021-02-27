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

        unit = _context.selectedUnit.GetComponent<Unit>();

        unit.ShowContextMenu();

        ContextMenuHandler.OnMove += ContextMenuHandler_OnMove; 
    }

    private void ContextMenuHandler_OnMove(object sender, System.EventArgs e)
    {
        _context.sm.changeState<MovingUnit>();
    }
}

public enum UnitAction
{
    Move,
    Attack,
    Both
}