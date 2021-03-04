using UnityEngine;
using Prime31.StateKit;

namespace Bodzio2k.BattleSystem
{
    class EnteringWinnigArea : SKState<BattleSystem>
    {
        public override void update(float deltaTime)
        {
            return;
        }

        public override void begin()
        {
            base.begin();

            _context.sm.changeState<Idle>();
        }

        public override void end()
        {
            base.end();
        }
    }
}