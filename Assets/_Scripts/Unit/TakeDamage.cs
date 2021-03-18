using Prime31.StateKit;
using UnityEngine;

namespace Bodzio2k.Unit
{
    public class TakeDamage : SKState<Unit>
    {
        public override void begin()
        {
            base.begin();

            _context.health -= _context.willReceiveDamage * _context.damageMultiplier;

            Debug.LogFormat("{0} received damage of {1}; damage multiplier {2}; health reiaminig {3}", _context.name, _context.willReceiveDamage, _context.damageMultiplier, _context.health);

            if (_context.health <= 0)
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