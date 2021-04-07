using System;
using UnityEngine;
using Prime31.StateKit;
using UnityEngine.Tilemaps;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Bodzio2k.Tiles;
using Bodzio2k.UI;
using UnityEngine.UI;

namespace Bodzio2k.BattleSystem
{
    using WinningArea = Dictionary<Unit.Unit, int>;

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
        public GameObject touchedUnit;

        [HideInInspector]
        public SKStateMachine<BattleSystem> sm;

        [SerializeField]
        public Tilemap grid;

        [HideInInspector]
        public GamePhase gamePhase;

        [HideInInspector]
        public GamePhase whoStarts;

        [HideInInspector]
        public GameMode gameMode = GameMode.ReleaseMode;

        [HideInInspector]
        public Vector3 cameraPosition;

        [SerializeField]
        public GameObject cameraFocus;

        [HideInInspector]
        public int currentRoundNumber = 1;

        [SerializeField]
        public int dominationRoundsCount = 1;

        [HideInInspector]
        public int playerOneUnitsRemaining = 5;

        [HideInInspector]
        public int playerTwoUnitsRemaining = 5;

        [HideInInspector]
        public int numberOfPlayerOneUnitsInWinningArea = 0;

        [HideInInspector]
        public int numberOfPlayerTwoUnitsInWinningArea = 0;

        [HideInInspector]
        public List<WinningAreaEntry> winningArea = new List<WinningAreaEntry>();

        [SerializeField]
        public Canvas mainMenu;

        [SerializeField]
        public Canvas canvas;

        private void Start()
        {
            sm = new SKStateMachine<BattleSystem>(this, new ShowingMainMenu());
            sm.addState(new Idle());
            sm.addState(new MovingUnit());
            sm.addState(new ChangeSide());
            sm.addState(new Aiming());
            sm.addState(new Inactive());
            sm.addState(new NewGame());
            sm.addState(new GameOver());
            sm.addState(new ShowingHowToPlay());

            UIHandler.OnSwitchMode += UIHandler_OnSwitchMode;
            UIHandler.OnNewGame += UIHandler_OnNewGame;
            UIHandler.OnQuit += UIHandler_OnQuit;
            UIHandler.OnHowToPlay += UIHandler_OnHowToPlay;
        }

        private void UIHandler_OnHowToPlay(object sender, EventArgs e)
        {
            sm.changeState<ShowingHowToPlay>();
        }

        private void UIHandler_OnQuit(object sender, EventArgs e)
        {
            Application.Quit();
        }

        private void UIHandler_OnNewGame(object sender, EventArgs e)
        {
            sm.changeState<NewGame>();
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
                    string gameModeDir = gameMode == GameMode.DesignerMode ? "DesignerMode" : "ReleaseMode";
                    string unitFilename = unit.gameObject.name;

                    string path = $@"Units\{gameModeDir}\{unitFilename}";

                    Debug.LogFormat("Loading {0}...", path);

                    sr.sprite = Resources.Load<Sprite>(path);

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

        public bool DidSomeoneWin()
        {
            bool didSomeoneWin = false;

            Debug.LogFormat("Checking winning conditions after round #{0}...", currentRoundNumber);

            numberOfPlayerOneUnitsInWinningArea = winningArea.Where(x => x.side == Side.PlayerOne && currentRoundNumber - dominationRoundsCount > x.roundEntered).Count();
            numberOfPlayerTwoUnitsInWinningArea = winningArea.Where(x => x.side == Side.PlayerTwo && currentRoundNumber - dominationRoundsCount > x.roundEntered).Count();

            if (numberOfPlayerOneUnitsInWinningArea > 0 && numberOfPlayerOneUnitsInWinningArea > numberOfPlayerTwoUnitsInWinningArea)
            {
                Debug.LogFormat("Player one won after round #{0}...", currentRoundNumber);

                playerTwoUnitsRemaining = 0;

                didSomeoneWin = true;
            }

            if (numberOfPlayerTwoUnitsInWinningArea > 0 && numberOfPlayerTwoUnitsInWinningArea > numberOfPlayerOneUnitsInWinningArea)
            {
                Debug.LogFormat("Player two won after round #{0}...", currentRoundNumber);

                playerOneUnitsRemaining = 0;

                didSomeoneWin = true;
            }

            if (numberOfPlayerOneUnitsInWinningArea == numberOfPlayerTwoUnitsInWinningArea)
            {
                Debug.LogFormat("Both sides have equal number of units in winning area after round #{0}...", currentRoundNumber);

                didSomeoneWin = false;
            }

            return didSomeoneWin;
        }

        public void RemoveFromWinningArea(Unit.Unit unit)
        {
            WinningAreaEntry leavedArea = winningArea.Where(x => x.unit == unit).First();

            winningArea.Remove(leavedArea);
        }
    }
}
