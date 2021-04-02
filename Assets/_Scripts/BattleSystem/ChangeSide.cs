using Prime31.StateKit;
using UnityEngine;
using System.Collections;
using TMPro;

namespace Bodzio2k.BattleSystem
{
    public class ChangeSide : SKState<BattleSystem>
    {
        private Unit.Unit unit;
        private GameObject roundAnnouncer;
        private GameObject roundNumber;
        private GameObject turnIndicator1;
        private GameObject turnIndicator2;

        public override void update(float deltaTime)
        {
            return;
        }

        public override void begin()
        {
            base.begin();
            
            GamePhase currentPhase = _context.gamePhase;
            GamePhase newPhase = currentPhase == GamePhase.PlayerOne ? GamePhase.PlayerTwo : GamePhase.PlayerOne;

            _context.gamePhase = newPhase;

            unit = _context.selectedUnit.GetComponent<Unit.Unit>();

            ResetUnitActions();

            _context.selectedUnit = null;
            _context.touchedUnit = null;

            _machine.changeState<Idle>();

            if (_context.gamePhase == _context.whoStarts)
            {
                _context.roundNumber++;

                Debug.LogFormat("Round {0} started...", _context.roundNumber);
            }

            UpdateUI();
        }

        private void ResetUnitActions()
        {
            unit.actionsRemaining = 2;
        }

        public override void end()
        {
            base.end();

            _context.CheckWinningCondtions();
        }

        private void UpdateUI()
        {
            roundAnnouncer = GameObject.Find("/UI/Canvas/RoundAnnouncer");
            roundAnnouncer.SetActive(true);

            string text = _context.gamePhase == GamePhase.PlayerOne ? "player one" : "player two";
            roundAnnouncer.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(text);

            roundNumber = GameObject.Find("/UI/Canvas/RoundNumber");
            text = $"round {_context.roundNumber}";
            roundNumber.GetComponent<TextMeshProUGUI>().SetText(text);

            turnIndicator1 = GameObject.Find("/UI/Canvas/PlayerOneTurnIndicator");
            turnIndicator2 = GameObject.Find("/UI/Canvas/PlayerTwoTurnIndicator");

            turnIndicator1.SetActive(_context.gamePhase == GamePhase.PlayerOne);
            turnIndicator2.SetActive(_context.gamePhase == GamePhase.PlayerTwo);

            _context.StartCoroutine(HideRoundAnnouncer());
        }

        IEnumerator HideRoundAnnouncer()
        {
            yield return new WaitForSeconds(2f);

            roundAnnouncer.SetActive(false);
        }
    }
}