using System;
using UnityEngine;
using Prime31.StateKit;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System.Linq;

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

        private void Start()
        {
            sm = new SKStateMachine<BattleSystem>(this, new Idle());
            sm.addState(new MovingUnit());
            sm.addState(new ChangeSide());
            sm.addState(new EnteringWinnigArea());
            sm.addState(new Aiming());

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
            //string subdirectory = gameMode == GameMode.DesignerMode ? "DesignerMode" : "PlayerMode";
            Bodzio2k.Unit.Unit unit;

            if (go.TryGetComponent<SpriteRenderer>(out sr))
            {
                if (go.TryGetComponent<Bodzio2k.Unit.Unit>(out unit))
                {
                    sr.sprite = unit.properties.sprites[(int) gameMode];
                }
            }
        }
    }
}
