using Prime31.StateKit;

namespace Bodzio2k.Unit
{
    public class Die : SKState<Unit>
    {
        public override void begin()
        {
            base.begin();

            _context.GetComponent<Unit>().Die();
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