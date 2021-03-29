using Prime31.StateKit;
using Bodzio2k.BattleSystem;

namespace Bodzio2k.Unit
{
    public class DecreaseActionCount : SKState<Unit>
    {
        private Unit unit;

        public override void begin()
        {
            base.begin();

            unit = _context.GetComponent<Unit>();

            unit.actionsRemaining = -1;

            //unit.sm.changeState<Bodzio2k.Unit.Idle>();
        }

        public override void update(float deltaTime)
        {
            return;
        }
    }
}