using UnityEngine;
using Prime31.StateKit;
using System.Collections;
using TMPro;

namespace Bodzio2k.BattleSystem
{
    class NewGame : SKState<BattleSystem>
    {
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

            _context.mainMenu.gameObject.SetActive(false);
            _context.canvas.gameObject.SetActive(true);

            _context.whoStarts = (GamePhase) new System.Random().Next(0, 2);
            _context.gamePhase = _context.whoStarts;

            UpdateUI();

            _context.StartCoroutine(HideRoundAnnouncer());

            _context.sm.changeState<Idle>();
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