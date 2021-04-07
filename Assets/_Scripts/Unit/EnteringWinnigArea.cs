using Prime31.StateKit;
using System.Collections.Generic;
using UnityEngine;
using Bodzio2k.BattleSystem;

namespace Bodzio2k.Unit
{
    class EnteringWinnigArea : SKState<Unit>
    {
        public override void update(float deltaTime)
        {
            return;
        }

        public override void begin()
        {
            base.begin();

            int roundNumber = _context.battleSystem.currentRoundNumber;

            //WinningArea enteringUnit = new WinningArea
            //{
            //    { _context, roundNumber }
            //};
            WinningAreaEntry enteringUnit = new WinningAreaEntry(_context, _context.side, roundNumber);
            _context.battleSystem.winningArea.Add(enteringUnit);

            //_context.sm.changeState<Unit.Idle>();

            Debug.LogFormat("{0} entered winning area on round #{1}...", _context.name, roundNumber);
        }

        public override void end()
        {
            base.end();
        }
    }
}