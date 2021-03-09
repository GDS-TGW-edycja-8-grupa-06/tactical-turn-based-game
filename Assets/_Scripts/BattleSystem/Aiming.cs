using UnityEngine;
using Prime31.StateKit;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using Bodzio2k.Unit;
using System;

namespace Bodzio2k.BattleSystem
{
    public class Aiming : SKState<BattleSystem>
    {
        private Unit.Unit unit;

        public override void update(float deltaTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 targetPosition = Vector3.zero;

                if (IsMoveValid(ref targetPosition))
                {
                    if (IsMoveWithinRange(targetPosition))
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

        private bool IsMoveValid(ref Vector3 targetPosition)
        {
            bool moveIsValid = false;
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);

            Vector3Int tilemapPosition = _context.grid.WorldToCell(mousePosition);

            Tile tile = _context.grid.GetTile<Tile>(tilemapPosition);
            LayerMask restrictedAreaMask = _context.restrictedArea;
            
            //if (!canStepOntoBlueTiles)
            //{
            //    restrictedAreaMask = restrictedAreaMask | _context.blueTiles;
            //}

            RaycastHit2D hit = Physics2D.Raycast(mousePosition2D, Vector2.zero, Mathf.Infinity, restrictedAreaMask);

            if (hit.collider == null)
            {
                targetPosition = new Vector3(tilemapPosition.x + 1, tilemapPosition.y, 0);

                moveIsValid = true;
            }
            else
            {
                moveIsValid = false;
            }

            return moveIsValid;
        }

        private bool IsMoveWithinRange(Vector3 targetPosition)
        {
            List<Vector3> availablePostions = unit.GetAvailablePositions(Unit.Action.Attack);

            if (availablePostions.Contains(targetPosition))
            {
                return true;
            }

            return false;
        }

        public override void begin()
        {
            base.begin();

            unit = _context.selectedUnit.GetComponent<Unit.Unit>();

            //canStepOntoBlueTiles = Array.Exists(unit.properties.tags, tag => tag == Tag.CanStepOntoBlueTiles);

            unit.CreateRangeOverlay(OverlayType.Attack);
        }

        public override void end()
        {
            base.end();

            unit.HideRangeOverlay();
            unit.HideContextMenu();

            unit.actionsRemaining.Remove(Unit.Action.Attack);
        }
    }
}