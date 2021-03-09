using UnityEngine;
using Prime31.StateKit;
using Bodzio2k.Unit;
using System;
using System.Linq;

namespace Bodzio2k.BattleSystem
{
    public class MovingUnit : SKState<BattleSystem>
    {
        private Unit.Unit unit;
        private bool canStepOntoBlueTiles = false;

        public override void update(float deltaTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 targetPosition = Vector3.zero;

                if (unit.IsMoveValid(ref targetPosition))
                {
                    if (unit.IsActionWithinRange(Unit.Action.Move, targetPosition))
                    {
                        _context.selectedUnit.transform.position = targetPosition;

                        unit.sm.changeState<Unit.Idle>();
                    }
                    else
                    {
                        return;
                    }

                    if (unit.actionsRemaining.Count == 0)
                    {
                        _machine.changeState<ChangeSide>();
                    }
                }
            }
        }

        public override void begin()
        {
            base.begin();

            unit = _context.selectedUnit.GetComponent<Unit.Unit>();

            canStepOntoBlueTiles = Array.Exists(unit.properties.tags, tag => tag == Tag.CanStepOntoBlueTiles);

            unit.CreateRangeOverlay(OverlayType.Move);
        }

        public override void end()
        {
            base.end();

            unit.HideRangeOverlay();
            unit.HideContextMenu();

            unit.actionsRemaining.Remove(Unit.Action.Move);
        }
    }
}