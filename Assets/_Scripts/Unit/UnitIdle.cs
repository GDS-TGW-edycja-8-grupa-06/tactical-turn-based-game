﻿using UnityEngine;
using Prime31.StateKit;

public class UnitIdle : SKState<Unit>
{
    public override void update(float deltaTime)
    {
        throw new System.NotImplementedException();
    }

    public override void begin()
    {
        base.begin();

        _context.HideContextMenu();
    }
}
