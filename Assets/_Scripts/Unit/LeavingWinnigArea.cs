﻿using Prime31.StateKit;
using UnityEngine;

namespace Bodzio2k.BattleSystem
{
    class LeavingWinnigArea : SKState<Unit.Unit>
    {
        public override void update(float deltaTime)
        {
            return;
        }

        public override void begin()
        {
            base.begin();

            int roundNumber = _context.battleSystem.roundNumber;

            RemoveFromWinningArea(_context);

            _context.sm.changeState<Unit.Idle>();

            Debug.LogFormat("{0} leaved winning area on round #{1}...", _context.name, roundNumber);
        }

        public override void end()
        {
            base.end();
        }

        private void RemoveFromWinningArea(Unit.Unit unit)
        {
            unit.battleSystem.winningArea.RemoveAll(u => u.ContainsKey(unit));
        }
    }
}