﻿using UnityEngine;
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
                Vector3 aimingPosition = Vector3.zero;

                if (IsAttackValid(ref aimingPosition))
                {
                    if (IsAttackWithinRange(aimingPosition))
                    {
                        GameObject hitten = GetTarget(aimingPosition);

                        unit.actionsRemaining.Remove(Unit.Action.Attack);
                        
                        
                    }

                    if (unit.actionsRemaining.Count == 0)
                    {
                        unit.sm.changeState<Unit.Inactive>();
                        _machine.changeState<ChangeSide>();
                    }
                    else
                    {
                        unit.sm.changeState<Unit.Idle>();
                    }
                }
            }
        }

        private bool IsAttackValid(ref Vector3 targetPosition)
        {
            bool attackIsValid = false;
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);

            Vector3Int tilemapPosition = _context.grid.WorldToCell(mousePosition);

            Tile tile = _context.grid.GetTile<Tile>(tilemapPosition);
            LayerMask restrictedAreaMask = _context.restrictedArea;
            
            RaycastHit2D hit = Physics2D.Raycast(mousePosition2D, Vector2.zero, Mathf.Infinity, restrictedAreaMask);

            if (hit.collider == null)
            {
                targetPosition = new Vector3(tilemapPosition.x + 1, tilemapPosition.y, 0);

                attackIsValid = true;
            }
            else
            {
                attackIsValid = false;
            }

            return attackIsValid;
        }

        private bool IsAttackWithinRange(Vector3 targetPosition)
        {
            List<Vector3> availablePostions = unit.GetAvailablePositions(Unit.Action.Attack);

            if (availablePostions.Contains(targetPosition))
            {
                return true;
            }

            return false;
        }

        private GameObject GetTarget(Vector3 position)
        {
            GameObject target = null;
            LayerMask opponentPlayerLayerMask = unit.battleSystem.gamePhase == GamePhase.PlayerOne ? unit.battleSystem.playerTwoLayerMask : unit.battleSystem.playerOneLayerMask;

            RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero, Mathf.Infinity, opponentPlayerLayerMask);

            if (hit.collider != null)
            {
                ;
            }

            return target;
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

            unit.battleSystem.touchedUnit = unit.transform.gameObject;
        }
    }
}