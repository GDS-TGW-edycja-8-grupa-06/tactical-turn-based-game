﻿using Prime31.StateKit;
using Bodzio2k.BattleSystem;
using UnityEngine;
using System.Linq;

namespace Bodzio2k.Unit
{
    class LeavingWinnigArea : SKState<Unit>
    {
        public override void update(float deltaTime)
        {
            return;
        }

        public override void begin()
        {
            base.begin();

            int roundNumber = _context.battleSystem.currentRoundNumber;

            RemoveFromWinningArea(_context);

            _context.sm.changeState<Idle>();

            Debug.LogFormat("{0} leaved winning area on round #{1}...", _context.name, roundNumber);
        }

        public override void end()
        {
            base.end();
        }

        private void RemoveFromWinningArea(Unit unit)
        {
            WinningAreaEntry leavedArea = _context.battleSystem.winningArea.Where(x => x.unit == unit).First();

            _context.battleSystem.winningArea.Remove(leavedArea);
        }
    }
}