using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class ChangeSide : SKState<BattleSystem>
{
    public override void update(float deltaTime)
    {
        return;
    }

    public override void begin()
    {
        base.begin();
        
        GamePhase currentPhase = _context.gamePhase;
        GamePhase newPhase = currentPhase == GamePhase.PlayerOne ? GamePhase.PlayerTwo : GamePhase.PlayerOne;

        _context.gamePhase = newPhase;
        
        _machine.changeState<Idle>();
    }
}
