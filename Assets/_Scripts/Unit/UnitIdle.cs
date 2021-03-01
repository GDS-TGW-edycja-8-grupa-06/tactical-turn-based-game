using UnityEngine;
using Prime31.StateKit;
using System;

public class UnitIdle : SKState<Unit>
{
    public override void update(float deltaTime)
    {
        return;
    }

    public override void begin()
    {
        base.begin();

        try
        {
            _context.battleSystem.sm.changeState<BattleSystemIdle>();
        }
        catch (NullReferenceException)
        {
            return;
        }
    }
}
