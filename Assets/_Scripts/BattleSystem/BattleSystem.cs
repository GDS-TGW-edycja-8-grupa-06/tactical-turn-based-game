using System;
using UnityEngine;
using Prime31.StateKit;
using UnityEngine.Tilemaps;
using System.Linq;
using System.Collections.Generic;

namespace Bodzio2k.BattleSystem
{
    public class BattleSystem : MonoBehaviour
    {
        [SerializeField]
        public LayerMask playerOneLayerMask;
        public LayerMask playerTwoLayerMask;
        public LayerMask restrictedArea;
        public LayerMask walkableArea;
        public LayerMask blueTiles;

        [HideInInspector]
        public GameObject selectedUnit;
        public GameObject touchedUnit;

        [HideInInspector]
        public SKStateMachine<BattleSystem> sm;

        [SerializeField]
        public Tilemap grid;

        [HideInInspector]
        public GamePhase gamePhase = GamePhase.PlayerOne;

        [HideInInspector]
        public GameMode gameMode = GameMode.ReleaseMode;

        [HideInInspector]
        public Vector3 cameraPosition;

        [SerializeField]
        public GameObject cameraFocus;

        [HideInInspector]
        public int roundNumber = 1;

        [HideInInspector]
        public Dictionary<Unit.Unit, int> winningArea = new Dictionary<Unit.Unit, int>();

        private void Start()
        {
            sm = new SKStateMachine<BattleSystem>(this, new Idle());
            sm.addState(new MovingUnit());
            sm.addState(new ChangeSide());
            sm.addState(new Aiming());
            sm.addState(new Inactive());

            UIHandler.OnSwitchMode += UIHandler_OnSwitchMode;
        }

        void Update()
        {
            sm.update(Time.deltaTime);
        }

        private void UIHandler_OnSwitchMode(object sender, EventArgs e)
        {
            GameObject[] objects = { };
            GameObject[] units = { };

            objects = GameObject.FindGameObjectsWithTag("Object");
            units = GameObject.FindGameObjectsWithTag("Player");

            gameMode = gameMode == GameMode.DesignerMode ? GameMode.ReleaseMode : GameMode.DesignerMode;

            foreach (GameObject go in objects.Concat(units))
            {
                ChangeSprite(go);
            }
        }

        private void ChangeSprite(GameObject go)
        {
            SpriteRenderer sr = null;
            Bodzio2k.Unit.Unit unit;
            GenericTile genericTile;

            if (go.TryGetComponent<SpriteRenderer>(out sr))
            {
                if (go.TryGetComponent<Bodzio2k.Unit.Unit>(out unit))
                {
                    sr.sprite = unit.properties.sprites[(int) gameMode];

                    return;
                }

                if (go.TryGetComponent<GenericTile>(out genericTile))
                {
                    sr.sprite = genericTile.sprites[(int)gameMode];

                    return;
                }
            }

            int childCount = go.transform.childCount;

            if (childCount > 0)
            {
                for (int i = 0; i < go.transform.childCount; i++)
                {
                    GameObject childGo = go.transform.GetChild(i).gameObject;

                    ChangeSprite(childGo);
                }
            }
        }
    }
}
