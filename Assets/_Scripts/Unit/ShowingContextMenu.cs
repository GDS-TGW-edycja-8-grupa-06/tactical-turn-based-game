using UnityEngine;
using Prime31.StateKit;
using Bodzio2k.BattleSystem;

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

                _context.sm.changeState<Idle>();
                _context.battleSystem.GetComponent<BattleSystem.BattleSystem>().sm.changeState<BattleSystem.Idle>();
            }
        }

        public override void begin()
        {
            base.begin();

            unit = _context.GetComponent<Unit>();

            unit.ShowContextMenu();

            ContextMenuHandler.OnMove += ContextMenuHandler_OnMove;
            ContextMenuHandler.OnAttack += ContextMenuHandler_OnAttack;
        }

        private void ContextMenuHandler_OnAttack(object sender, System.EventArgs e)
        {
            _context.battleSystem.sm.changeState<Aiming>();
        }

        private void ContextMenuHandler_OnMove(object sender, System.EventArgs e)
        {
            _context.battleSystem.sm.changeState<MovingUnit>();
        }
    }
}