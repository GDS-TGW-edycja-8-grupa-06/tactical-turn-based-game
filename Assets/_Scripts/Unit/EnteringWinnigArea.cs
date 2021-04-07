using Prime31.StateKit;
using System.Linq;
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

            WinningAreaEntry enteringUnit = new WinningAreaEntry(_context, _context.side, _context.battleSystem.currentRoundNumber);
            AddToWinningAreaList(enteringUnit);

            Debug.LogFormat("{0} entered winning area on round #{1}...", _context.name, _context.battleSystem.currentRoundNumber);
        }

        public override void end()
        {
            base.end();
        }

        private void AddToWinningAreaList(WinningAreaEntry entry)
        {
            _context.battleSystem.winningArea.Add(entry);

            Side opponentSide = entry.side == Side.PlayerOne ? Side.PlayerTwo : Side.PlayerOne;

            var opponentEntries = _context.battleSystem.winningArea.Where(x => x.side == opponentSide);

            if (opponentEntries.Count() > 0)
            {
                foreach (WinningAreaEntry item in _context.battleSystem.winningArea)
                {
                    item.roundEntered = _context.battleSystem.currentRoundNumber;
                }
            }
        }
    }
}