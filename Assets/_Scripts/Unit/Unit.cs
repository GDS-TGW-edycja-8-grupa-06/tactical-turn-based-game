using System.Collections.Generic;
using UnityEngine;
using Prime31.StateKit;
using System.Linq;
using System;
using UnityEngine.Tilemaps;
using Bodzio2k.BattleSystem;
using TMPro;

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
        public int actionsRemaining;

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

        [HideInInspector]
        public float willReceiveDamage = 0.0f;

        [HideInInspector]
        public float health;

        [HideInInspector]
        public float totalHealth;

        [HideInInspector]
        public float damageMultiplier = 1.0f;

        [HideInInspector]
        public GameObject healthBar;

        [HideInInspector]
        public GameObject healthBarGauge;

        [HideInInspector]
        public bool isImmobilized = false;

        private SpriteRenderer sr;

        private void Start()
        {
            battleSystem = GameObject.Find("/BattleSystem").GetComponent<BattleSystem.BattleSystem>();

            sm = new SKStateMachine<Unit>(this, new Idle());

            sm.addState(new ShowingContextMenu());
            sm.addState(new ChangeDamageMultiplier());
            sm.addState(new TakeDamage());
            sm.addState(new DisableAttack());
            sm.addState(new Inactive());
            sm.addState(new Die());
            sm.addState(new DecreaseActionCount());
            sm.addState(new EnteringWinnigArea());
            sm.addState(new LeavingWinnigArea());

            overlay = transform.Find("Overlay").gameObject;

            canStepOntoBlueTiles = Array.Exists(properties.tags, tag => tag == Tag.CanStepOntoBlueTiles);

            sr = GetComponent<SpriteRenderer>();

            LoadSprites();

            health = properties.health;
            totalHealth = properties.health;

            return;
        }

        public void ShowContextMenu()
        {
            if (contextMenu != null)
            {
                Vector3 position = GetContextMenuPostion();

                contextMenuInstance = Instantiate(contextMenu, position, Quaternion.identity);

                contextMenuHandler = contextMenuInstance.GetComponent<ContextMenuHandler>();

                return;
            }

            return;
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
            int range = overlayType == OverlayType.Move ? properties.moveRange : properties.attackRange;

            List<Vector3> overlayPositions = GetOverlayPositions(overlayType, range);

            foreach (Vector3 position in overlayPositions)
            {
                InstantiateOverlayTile(position, overlayType);
            }

            PostCreateOverlay(overlayType);

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

        private List<Vector3> GetOverlayPositions(OverlayType overlayType, int range)
        {
            List<Vector3> positions = new List<Vector3>();
            IEnumerable<int> ranges = Enumerable.Range(-range, range * 2 + 1);

            if (overlayType == OverlayType.Move)
            {
                foreach (int h in ranges)
                {
                    positions.Add(new Vector3(h, 0, transform.position.z));
                }

                foreach (int v in ranges)
                {
                    positions.Add(new Vector3(0, v, transform.position.z));
                }
            }

            if (overlayType == OverlayType.Attack)
            {
                foreach (int h in ranges)
                {
                    foreach (int v in ranges)
                    {
                        positions.Add(new Vector3(h, v, transform.position.z));
                    }
                }
            }

            return positions;
        }

        private void PostCreateOverlay(OverlayType overlayType)
        {
            bool active;

            for (int i = 0; i < overlay.transform.childCount; i++)
            {
                GameObject overlayChild = overlay.transform.GetChild(i).gameObject;

                if (overlayType == OverlayType.Move)
                {
                    active = !IsTileOccupied(overlayChild.transform.position);
                }
                else
                {
                    active = IsTileOccupiedByEnemyUnit(overlayChild.transform.position);
                }

                overlayChild.SetActive(active);
            }
        }

        public List<Vector3> GetAvailablePositions(Action action)
        {
            List<Vector3> availablePostions = new List<Vector3>();
            Vector3 pawnPostion = transform.position;
            int range = action == Action.Attack ? properties.attackRange : properties.moveRange;

            for (int x = 1; x < range + 1; x++)
            {
                for (int y = 1; y < range + 1; y++)
                {
                    availablePostions.Add(new Vector3(pawnPostion.x + x, pawnPostion.y));
                    availablePostions.Add(new Vector3(pawnPostion.x + -x, pawnPostion.y));
                    availablePostions.Add(new Vector3(pawnPostion.x, pawnPostion.y + y));
                    availablePostions.Add(new Vector3(pawnPostion.x, pawnPostion.y + -y));
                }
            }

            return availablePostions;
        }

        public bool IsActionWithinRange(Action action, Vector3 targetPosition)
        {
            List<Vector3> availablePostions = GetAvailablePositions(Action.Move);
            
            if (availablePostions.Any(postion => postion == targetPosition))
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

        public bool IsAttackValid(ref Vector3 targetPosition)
        {
            bool attackIsValid = false;
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);

            attackIsValid = IsTileOccupiedByEnemyUnit(mousePosition2D);

            targetPosition = new Vector3(mousePosition2D.x, mousePosition2D.y, 0);

            return attackIsValid;
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

        private bool IsTileOccupiedByEnemyUnit(Vector2 postion)
        {
            LayerMask allowedLayerMask = battleSystem.gamePhase == GamePhase.PlayerOne ? battleSystem.playerTwoLayerMask : battleSystem.playerOneLayerMask;

            RaycastHit2D hit = Physics2D.Raycast(postion, Vector2.zero, Mathf.Infinity, allowedLayerMask);

            if (hit.collider != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Die()
        {
            GameObject label;
            int unitsRemaining;
            string playerSide;
            
            if (side == Side.PlayerOne)
            {
                battleSystem.playerOneUnitsRemaining--;
                unitsRemaining = battleSystem.playerOneUnitsRemaining;
                playerSide = "PlayerOne";
            }
            else
            {
                battleSystem.playerTwoUnitsRemaining--;
                unitsRemaining = battleSystem.playerTwoUnitsRemaining;
                playerSide = "PlayerTwo";
            }

            label = GameObject.Find($"/UI/Canvas/{playerSide}UnitsRemaining");
            label.GetComponent<TextMeshProUGUI>().SetText($"units remaining {unitsRemaining}");

            this.gameObject.SetActive(false);
        }

        private void Update()
        {
            sm.update(Time.deltaTime);
        }

        private void LoadSprites()
        {
            moveRangeSpriteOverlay = Resources.Load<Sprite>("MovingRangeOverlay");
            attackRangeSpriteOverlay = Resources.Load<Sprite>("AimingRangeOverlay");

            string gameModeDir = battleSystem.gameMode == GameMode.DesignerMode ? "DesignerMode" : "ReleaseMode";
            string unitFilename = gameObject.name;

            string path = $@"Units\{gameModeDir}\{unitFilename}";
            
            sr.sprite = Resources.Load<Sprite>(path);
        }

        private void OnMouseExit()
        {
            healthBar.SetActive(false);
        }

        private void OnMouseEnter()
        {
            healthBar.SetActive(true);
        }
    }
}