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

                        if (unit.action == Bodzio2k.Unit.Action.Both)
                        {
                            unit.action = Bodzio2k.Unit.Action.Attack;
                        }
                    }
                    else
                    {
                        return;
                    }

                    _machine.changeState<ChangeSide>();
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
            List<Vector3> availablePostions = GetAvailablePositions(_context.selectedUnit);

            if (availablePostions.Contains(targetPosition))
            {
                return true;
            }

            return false;
        }

        private List<Vector3> GetAvailablePositions(GameObject pawn)
        {
            List<Vector3> availablePostions = new List<Vector3>();
            Vector3 pawnPostion = pawn.transform.position;
            Properties unitProperties = unit.properties;
            int attackRange = unitProperties.attackRange;

            availablePostions.Add(new Vector3(pawnPostion.x + attackRange, pawnPostion.y));
            availablePostions.Add(new Vector3(pawnPostion.x + -attackRange, pawnPostion.y));
            availablePostions.Add(new Vector3(pawnPostion.x, pawnPostion.y + attackRange));
            availablePostions.Add(new Vector3(pawnPostion.x, pawnPostion.y + -attackRange));

            return availablePostions;
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
        }
    }
}