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
        private Vector3 targetPosition;
        private Rigidbody2D rb;
        private bool moveIsValid = false;
        private bool moveIsWithinRange = false;

        public override void update(float deltaTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                moveIsValid = unit.IsMoveValid(ref targetPosition);
                moveIsWithinRange = unit.IsActionWithinRange(Unit.Action.Move, targetPosition);

                return;
            }

            Move();

            if (_context.selectedUnit.transform.position == targetPosition)
            {
                unit.actionsRemaining--;
                unit.battleSystem.touchedUnit = unit.transform.gameObject;

                if (unit.actionsRemaining == 0)
                {
                    unit.sm.changeState<Unit.Inactive>();
                    _machine.changeState<ChangeSide>();
                }
                else
                {
                    unit.sm.changeState<Unit.Idle>();
                }

                targetPosition = Vector3.zero;
            }
        }


        private void Move()
        {
            GameObject selectedUnit = _context.selectedUnit;

            Vector3 moveTo = selectedUnit.transform.position + (targetPosition * 10f * Time.deltaTime);

            if (moveIsValid && moveIsWithinRange && (targetPosition != Vector3.zero))
            {
                //_context.selectedUnit.transform.position = targetPosition;
                //rb.MovePosition(moveTo);

                //if (selectedUnit.transform.position == moveTo)
                //{
                //    targetPosition = Vector3.zero;
                //}
                //_context.selectedUnit.transform.Translate(targetPosition * 1f * Time.deltaTime);
            }
        }
    
        public override void begin()
        {
            base.begin();

            unit = _context.selectedUnit.GetComponent<Unit.Unit>();
            rb = _context.selectedUnit.GetComponent<Rigidbody2D>();

            canStepOntoBlueTiles = Array.Exists(unit.properties.tags, tag => tag == Tag.CanStepOntoBlueTiles);

            unit.CreateRangeOverlay(OverlayType.Move);
        }

        public override void end()
        {
            base.end();

            unit.HideRangeOverlay();
            unit.HideContextMenu();
        }
    }
}