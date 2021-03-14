using System;
using Prime31.StateKit;

namespace Bodzio2k.Unit
{
    public class Die : SKState<Unit>
    {
        public override void begin()
        {
            base.begin();

            Destroy(_context);
        }

        private void Destroy(Unit context)
        {
            throw new NotImplementedException();
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