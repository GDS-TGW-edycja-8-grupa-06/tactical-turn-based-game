using Prime31.StateKit;
using UnityEngine;

namespace Bodzio2k.Unit
{
    public class TakeDamage : SKState<Unit>
    {
        public override void begin()
        {
            base.begin();

            PlayAudio();

            _context.health -= _context.willReceiveDamage * _context.damageMultiplier;

            Debug.LogFormat("{0} received damage of {1}; damage multiplier {2}; health reiaminig {3}", _context.name, _context.willReceiveDamage, _context.damageMultiplier, _context.health);

            UpdateHealthBar();

            if (_context.health <= 0)
            {
                _context.sm.changeState<Die>();
            }
            else
            {
                _context.sm.changeState<Idle>();
            }
        }

        public override void update(float deltaTime)
        {
            return;
        }

        private void UpdateHealthBar()
        {
            if (_context.healthBarGauge != null)
            {
                _context.healthBarGauge.transform.localScale = new Vector2(_context.health / _context.totalHealth, _context.healthBarGauge.transform.localScale.y);
            }
        }

        private void PlayAudio()
        {
            _context.audioSource.clip = _context.takeDamageClip;
            _context.audioSource.Play();
        }
    }
}