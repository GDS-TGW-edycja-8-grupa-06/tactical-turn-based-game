using Prime31.StateKit;
using System.Collections.Generic;
using UnityEngine;

namespace Bodzio2k.BattleSystem
{
    class EnteringWinnigArea : SKState<Unit.Unit>
    {
        public override void update(float deltaTime)
        {
            return;
        }

        public override void begin()
        {
            base.begin();

            int roundNumber = _context.battleSystem.roundNumber;
            Dictionary<Unit.Unit, int> enteringUnit = new Dictionary<Unit.Unit, int>
            {
                { _context, roundNumber }
            };

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