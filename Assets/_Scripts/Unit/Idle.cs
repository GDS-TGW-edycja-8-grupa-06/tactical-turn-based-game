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

            if (_context.actionsRemaining == 0)
            {
                _context.actionsRemaining = 2;
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
