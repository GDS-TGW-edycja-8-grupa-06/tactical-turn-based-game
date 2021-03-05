using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31.StateKit;
using UnityEngine.Tilemaps;

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
        }

        void Update()
        {
            sm.update(Time.deltaTime);
        }
    }
}
