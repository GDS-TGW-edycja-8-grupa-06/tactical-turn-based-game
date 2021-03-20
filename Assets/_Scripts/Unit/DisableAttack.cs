using Prime31.StateKit;
using Bodzio2k.BattleSystem;

namespace Bodzio2k.Unit
{
    public class DisableAttack : SKState<Unit>
    {
        public override void begin()
        {
            base.begin();

            _context.battleSystem.sm.changeState<ChangeSide>();
        }

        public override void update(float deltaTime)
        {
            return;
        }
    }
}