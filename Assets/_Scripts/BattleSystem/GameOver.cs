using UnityEngine;
using Prime31.StateKit;
using Bodzio2k.BattleSystem;
using TMPro;

namespace Bodzio2k.BattleSystem
{
    class GameOver : SKState<BattleSystem>
    {
        private GameObject roundAnnouncer;

        public override void begin()
        {
            base.begin();

            UpdateUI();
        }

        public override void update(float deltaTime)
        {
            if (Input.anyKey)
            {
                _machine.changeState<ShowingMainMenu>();
            }
        }

        private void UpdateUI()
        {
            roundAnnouncer = GameObject.Find("/UI/Canvas/RoundAnnouncer");
            roundAnnouncer.SetActive(true);

            string text = _context.playerOneUnitsRemaining > 0 ? "player one wins" : "player two wins";
            roundAnnouncer.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(text);

            text = "press any key to return to main menu";
            roundAnnouncer.transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText(text);
        }
    }
}
