using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class ShowingContextMenu : SKState<Unit>
{
    private Unit unit;
    
    public override void update(float deltaTime)
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {           
            _context.HideContextMenu();

            _context.sm.changeState<UnitIdle>();
            _context.battleSystem.GetComponent<BattleSystem>().sm.changeState<BattleSystemIdle>();
        }
    }

    public override void begin()
    {
        base.begin();

        unit = _context.GetComponent<Unit>();
        
        unit.ShowContextMenu();

        ContextMenuHandler.OnMove += ContextMenuHandler_OnMove; 
    }

    private void ContextMenuHandler_OnMove(object sender, System.EventArgs e)
    {
        _context.battleSystem.sm.changeState<MovingUnit>();
    }


}

public enum UnitAction
{
    Move,
    Attack,
    Both
}