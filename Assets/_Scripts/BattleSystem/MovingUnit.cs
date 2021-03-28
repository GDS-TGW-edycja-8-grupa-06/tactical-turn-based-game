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
        private bool didMove;

        public override void update(float deltaTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                moveIsValid = unit.IsMoveValid(ref targetPosition);
                moveIsWithinRange = unit.IsActionWithinRange(Unit.Action.Move, targetPosition);
                didMove = false;

                Debug.LogFormat("{0} wants to move from {1} to {2}...", unit.gameObject.name, _context.selectedUnit.transform.position, targetPosition);

                return;
            }

            Move();

            //if (_context.selectedUnit.transform.position == targetPosition)
            if (didMove)
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

                targetPosition = Vector3.zero; targetPosition = Vector3.zero;

                didMove = false;
            }
        }


        private void Move()
        {
            GameObject selectedUnit = _context.selectedUnit;
            Vector3 startPostion = selectedUnit.transform.position;

            Vector3 moveTo = selectedUnit.transform.position + (targetPosition * 1000f * Time.deltaTime);

            if (moveIsValid && moveIsWithinRange && (targetPosition != Vector3.zero))
            {
                //selectedUnit.transform.position = Vector3.Lerp(startPostion, targetPosition, 10f * Time.deltaTime);
                rb.MovePosition(targetPosition);

                unit.HideRangeOverlay();
                unit.HideContextMenu();

                Debug.LogFormat("{0} did move to {1}...", _context.selectedUnit.name, targetPosition);

                didMove = true;

                return;
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
    }
}