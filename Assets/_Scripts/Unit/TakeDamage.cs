using Prime31.StateKit;

namespace Bodzio2k.Unit
{
    public class TakeDamage : SKState<Unit>
    {
        public override void begin()
        {
            base.begin();

            _context.sm.changeState<Idle>();
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