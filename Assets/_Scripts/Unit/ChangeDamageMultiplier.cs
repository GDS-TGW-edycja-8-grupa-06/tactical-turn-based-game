using Prime31.StateKit;
using Bodzio2k.Unit;

namespace Bodzio2k.Unit
{
    public class ChangeDamageMultiplier : SKState<Unit>
    {
        public override void begin()
        {
            base.begin();

            _context.damageMultiplier = _context.damageMultiplier == 1.0f ? 0.5f : 1.0f;
        }

        public override void end()
        {
            base.end();
        }

        public override void update(float deltaTime)
        {
            return;
        }
    }
}