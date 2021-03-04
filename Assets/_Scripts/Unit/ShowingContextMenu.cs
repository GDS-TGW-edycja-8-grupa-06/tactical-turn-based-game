using UnityEngine;
using Prime31.StateKit;
using Bodzio2k.BattleSystem;
using Bodzio2k.Unit;

namespace Bodzio2k.Unit
{
    public class ShowingContextMenu : SKState<Unit>
    {
        private Unit unit = null;

        public override void update(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _context.HideContextMenu();

                _context.sm.changeState<Bodzio2k.Unit.Idle>();
                _context.battleSystem.GetComponent<BattleSystem.BattleSystem>().sm.changeState<Bodzio2k.BattleSystem.Idle>();
            }
        }

        public override void begin()
        {
            base.begin();

            unit = _context.GetComponent<Unit>();

            unit.ShowContextMenu();

            ContextMenuHandler.OnMove += ContextMenuHandler_OnMove;
        }

        private void ContextMenuHandler_OnMove(object sender, System.EventArgs e)
        {
            _context.battleSystem.sm.changeState<MovingUnit>();
        }


    }
}