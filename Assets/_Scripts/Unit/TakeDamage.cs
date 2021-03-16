using Prime31.StateKit;
using UnityEngine;

namespace Bodzio2k.Unit
{
    public class TakeDamage : SKState<Unit>
    {
        public override void begin()
        {
            base.begin();

            _context.properties.health -= _context.willReceiveDamage;

            Debug.LogFormat("{0} received damage of {1}; health reiaminig {2}", _context.name, _context.willReceiveDamage, _context.properties.health);

            if (_context.properties.health <= 0)
            {
                _context.sm.changeState<Die>();
            }
            else
            {
                _context.sm.changeState<Idle>();
            }
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