﻿using Bodzio2k.BattleSystem;
using Prime31.StateKit;

namespace Bodzio2k.Unit
{
    public class Die : SKState<Unit>
    {
        public override void begin()
        {
            base.begin();

            PlayAudio();

            _context.GetComponent<Unit>().Die();
            
            if (_context.battleSystem.playerOneUnitsRemaining == 0 || _context.battleSystem.playerTwoUnitsRemaining == 0)
            {
                _context.battleSystem.sm.changeState<GameOver>();
            }
        }

        public override void update(float deltaTime)
        {
            return;
        }

        private void PlayAudio()
        {
            _context.audioSource.clip = _context.dieClip;
            _context.audioSource.Play();
        }
    }
}