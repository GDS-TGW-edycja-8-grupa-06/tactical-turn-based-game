using System.Collections.Generic;
using UnityEngine;
using Prime31.StateKit;
using System.Linq;
using System;

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
        public Action action;

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

        private void Start()
        {
            battleSystem = GameObject.Find("/BattleSystem").GetComponent<BattleSystem.BattleSystem>();

            sm = new SKStateMachine<Unit>(this, new Idle());

            sm.addState(new ShowingContextMenu());
            sm.addState(new ChangeDamageMultiplier());
            sm.addState(new TakeDamage());
            sm.addState(new DisableAttack());

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
            }
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

        //private bool IsOverlayAlreadyCreated()
        //{
        //    if (overlay == null || overlay.transform.childCount == 0)
        //    {
        //        return false;
        //    }

        //    overlay.SetActive(true);

        //    return true;
        //}

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