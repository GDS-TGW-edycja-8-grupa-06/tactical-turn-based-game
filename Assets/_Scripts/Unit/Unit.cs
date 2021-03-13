using System.Collections.Generic;
using UnityEngine;
using Prime31.StateKit;
using System.Linq;
using System;
using UnityEngine.Tilemaps;

namespace Bodzio2k.Unit
{
    public class Unit : MonoBehaviour
    {
        [SerializeField]
        public Properties properties;

        [SerializeField]
        public Side side;

        [SerializeField]
        public GameObject contextMenu;
        private GameObject contextMenuInstance;

        [HideInInspector]
        public List<Action> actionsRemaining;

        [SerializeField]
        public GameObject overlayTilePrefab;

        [HideInInspector]
        public SKStateMachine<Unit> sm;

        [HideInInspector]
        public BattleSystem.BattleSystem battleSystem;

        private GameObject overlay;
        private Sprite moveRangeSpriteOverlay;
        private Sprite attackRangeSpriteOverlay;

        private bool canStepOntoBlueTiles = false;

        private ContextMenuHandler contextMenuHandler;

        private void Start()
        {
            battleSystem = GameObject.Find("/BattleSystem").GetComponent<BattleSystem.BattleSystem>();

            sm = new SKStateMachine<Unit>(this, new Idle());

            sm.addState(new ShowingContextMenu());
            sm.addState(new ChangeDamageMultiplier());
            sm.addState(new TakeDamage());
            sm.addState(new DisableAttack());
            sm.addState(new Inactive());

            overlay = transform.Find("Overlay").gameObject;

            canStepOntoBlueTiles = Array.Exists(properties.tags, tag => tag == Tag.CanStepOntoBlueTiles);

            LoadSprites();

            return;
        }

        public void ShowContextMenu()
        {
            if (contextMenu != null)
            {
                Vector3 position = GetContextMenuPostion();

                contextMenuInstance = Instantiate(contextMenu, position, Quaternion.identity);

                contextMenuHandler = contextMenuInstance.GetComponent<ContextMenuHandler>();

                EnableActions();

                return;
            }

            return;
        }

        private void EnableActions()
        {
            contextMenuHandler.buttonMove.enabled = actionsRemaining.Contains(Action.Move);
            contextMenuHandler.buttonAttack.enabled = actionsRemaining.Contains(Action.Attack);
        }

        public void HideContextMenu()
        {
            if (contextMenuInstance != null)
            {
                Destroy(contextMenuInstance);
            }
        }

        private Vector3 GetContextMenuPostion()
        {
            Vector3 position = transform.position;

            position.y = position.y == -7.0f ? -6.0f : position.y;

            return transform.position;
        }

        public void CreateRangeOverlay(OverlayType overlayType)
        {
            //if (IsOverlayAlreadyCreated())
            //{
            //    PostCreateOverlay();

            //    return;
            //}

            int range = overlayType == OverlayType.Move ? properties.moveRange : properties.attackRange;
        
            List<Vector3> overlayPositions = GetOverlayPositions(range);

            foreach (Vector3 position in overlayPositions)
            {
                InstantiateOverlayTile(position, overlayType);
            }

            PostCreateOverlay();

            return;
        }

        private void InstantiateOverlayTile(Vector3 position, OverlayType overlayType)
        {
            Sprite sprite = overlayType == OverlayType.Move ? moveRangeSpriteOverlay : attackRangeSpriteOverlay;

            GameObject go = Instantiate(overlayTilePrefab, overlay.transform.position + position, Quaternion.identity, overlay.transform);
            go.GetComponent<SpriteRenderer>().sprite = sprite;
        }

        public void HideRangeOverlay()
        {
            GameObject overlay = transform.Find("Overlay").gameObject;

            for (int i = 0; i < overlay.transform.childCount; i++)
            {
                Destroy(overlay.transform.GetChild(i).gameObject);
            }
        }

        private List<Vector3> GetOverlayPositions(int range)
        {
            List<Vector3> positions = new List<Vector3>();
            IEnumerable<int> ranges = Enumerable.Range(-range, range * 2 + 1).Where(r => r == -range || r == range);

            foreach (int h in ranges)
            {
                positions.Add(new Vector3(h, 0, transform.position.z));
            }

            foreach (int v in ranges)
            {
                positions.Add(new Vector3(0, v, transform.position.z));
            }

            return positions;
        }

        private void PostCreateOverlay()
        {
            for (int i = 0; i < overlay.transform.childCount; i++)
            {
                GameObject overlayChild = overlay.transform.GetChild(i).gameObject;

                if (IsTileOccupied(overlayChild.transform.position))
                {
                    overlayChild.SetActive(false);
                }
                else
                {
                    overlayChild.SetActive(true);
                }
            }
        }

        public List<Vector3> GetAvailablePositions(Action action)
        {
            List<Vector3> availablePostions = new List<Vector3>();
            Vector3 pawnPostion = transform.position;
            int range = action == Action.Attack ? properties.attackRange : properties.moveRange;

            availablePostions.Add(new Vector3(pawnPostion.x + range, pawnPostion.y));
            availablePostions.Add(new Vector3(pawnPostion.x + -range, pawnPostion.y));
            availablePostions.Add(new Vector3(pawnPostion.x, pawnPostion.y + range));
            availablePostions.Add(new Vector3(pawnPostion.x, pawnPostion.y + -range));

            return availablePostions;
        }

        public bool IsActionWithinRange(Action action, Vector3 targetPosition)
        {
            List<Vector3> availablePostions = GetAvailablePositions(Action.Move);

            if (availablePostions.Contains(targetPosition))
            {
                return true;
            }

            return false;
        }

        public bool IsMoveValid(ref Vector3 targetPosition)
        {
            bool moveIsValid = false;
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);
            
            Vector3Int tilemapPosition = battleSystem.grid.WorldToCell(mousePosition);

            Tile tile = battleSystem.grid.GetTile<Tile>(tilemapPosition);
            LayerMask restrictedAreaMask = battleSystem.restrictedArea;

            if (!canStepOntoBlueTiles)
            {
                restrictedAreaMask = restrictedAreaMask | battleSystem.blueTiles;
            }

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

        private bool IsTileOccupied(Vector2 postion)
        {
            LayerMask forbiddenMoves = battleSystem.playerOneLayerMask | battleSystem.playerTwoLayerMask;
        
            if (!canStepOntoBlueTiles)
            {
                forbiddenMoves = forbiddenMoves | battleSystem.blueTiles;
            }

            RaycastHit2D hit = Physics2D.Raycast(postion, Vector2.zero, Mathf.Infinity, forbiddenMoves);

            if (hit.collider != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Update()
        {
            sm.update(Time.deltaTime);
        }

        private void LoadSprites()
        {
            moveRangeSpriteOverlay = Resources.Load<Sprite>("MovingRangeOverlay");
            attackRangeSpriteOverlay = Resources.Load<Sprite>("AimingRangeOverlay");
        }
    }
}