using UnityEngine;
using Prime31.StateKit;

class BattleSystemEnteringWinnigArea : SKState<BattleSystem>
{
    public override void update(float deltaTime)
    {
        return;
    }

    public override void begin()
    {
        base.begin();

        _context.sm.changeState<BattleSystemIdle>();
    }

    public override void end()
    {
        base.end();
    }
}