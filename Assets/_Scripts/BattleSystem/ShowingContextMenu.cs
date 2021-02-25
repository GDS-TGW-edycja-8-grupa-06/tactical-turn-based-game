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
        }
    }

    public override void begin()
    {
        base.begin();

        //_machine.changeState<MovingPawn>();
        unit = _context.selectedPawn.GetComponent<Unit>();

        unit.ShowContextMenu();
    }
}
