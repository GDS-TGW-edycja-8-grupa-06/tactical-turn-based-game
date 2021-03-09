using System;
using Prime31.StateKit;
using Bodzio2k.BattleSystem;

namespace Bodzio2k.Unit
{
    public class Idle : SKState<Unit>
    {
        public override void update(float deltaTime)
        {
            return;
        }

        public override void begin()
        {
            base.begin();

            if (_context.actionsRemaining.Count == 0)
            {
                _context.actionsRemaining.Add(Action.Attack);
                _context.actionsRemaining.Add(Action.Move);
            }

            try
            {
                _context.battleSystem.sm.changeState<BattleSystem.Idle>();
            }
            catch (NullReferenceException)
            {
                return;
            }

            return;
        }
    }
}
