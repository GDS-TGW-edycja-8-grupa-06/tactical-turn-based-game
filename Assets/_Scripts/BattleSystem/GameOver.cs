using UnityEngine;
using Prime31.StateKit;
using Bodzio2k.BattleSystem;
using TMPro;

namespace Bodzio2k.BattleSystem
{
    class GameOver : SKState<BattleSystem>
    {
        private GameObject roundAnnouncer;
        private float delay = 0.0f;
        private float waitFor = 1.0f;
        private bool skip = false;

        public override void begin()
        {
            base.begin();

            UpdateUI();
        }

        public override void update(float deltaTime)
        {
            delay += deltaTime;

            if(delay > waitFor && !skip)
            {
                string text = "press any key to return to main menu";
                roundAnnouncer.transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText(text);

                skip = true;
            }

            if (Input.anyKey && delay > waitFor)
            {
                Debug.LogFormat("Waited for {0} secs...", delay);

                _machine.changeState<ShowingMainMenu>();
            }
        }

        private void UpdateUI()
        {
            roundAnnouncer = GameObject.Find("/UI/Canvas/RoundAnnouncer");
            roundAnnouncer.SetActive(true);

            string text = _context.playerOneUnitsRemaining > 0 ? "player one wins" : "player two wins";
            roundAnnouncer.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(text);

            text = "";
            roundAnnouncer.transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText(text);
        }
    }
}
