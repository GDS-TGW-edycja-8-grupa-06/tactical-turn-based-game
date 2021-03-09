using Prime31.StateKit;
using Bodzio2k.Unit;

namespace Bodzio2k.BattleSystem
{
    public class ChangeSide : SKState<BattleSystem>
    {
        private Unit.Unit unit;

        public override void update(float deltaTime)
        {
            return;
        }

        public override void begin()
        {
            base.begin();

            GamePhase currentPhase = _context.gamePhase;
            GamePhase newPhase = currentPhase == GamePhase.PlayerOne ? GamePhase.PlayerTwo : GamePhase.PlayerOne;

            _context.gamePhase = newPhase;

            unit = _context.selectedUnit.GetComponent<Unit.Unit>();

            ResetUnitActions();

            _context.selectedUnit = null;

            _machine.changeState<Idle>();
        }

        private void ResetUnitActions()
        {
            unit.actionsRemaining.Add(Action.Attack);
            unit.actionsRemaining.Add(Action.Move);
        }
    }
}