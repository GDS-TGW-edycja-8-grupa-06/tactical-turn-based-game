using UnityEngine;
using Prime31.StateKit;
using System.Collections;
using TMPro;

namespace Bodzio2k.BattleSystem
{
    class ShowingHowToPlay : SKState<BattleSystem>
    {
        private GameObject howToPlayText;

        public override void update(float deltaTime)
        {
            if (Input.anyKey)
            {
                _context.sm.changeState<ShowingMainMenu>();
            };
        }

        public override void begin()
        {
            base.begin();

            UpdateUI(true);
        }

        public override void end()
        {
            base.end();

            UpdateUI(false);
        }

        private void UpdateUI(bool showingOrNotShowing)
        {
            GameObject.Find("/UI/MainMenu/HowToPlayText").SetActive(showingOrNotShowing);

            GameObject.Find("/UI/MainMenu/WelcomeTo").SetActive(!showingOrNotShowing);
            GameObject.Find("/UI/MainMenu/MadCemetary").SetActive(!showingOrNotShowing);
            GameObject.Find("/UI/MainMenu/NewGame").SetActive(!showingOrNotShowing);
            GameObject.Find("/UI/MainMenu/HowToPlay").SetActive(!showingOrNotShowing);
            GameObject.Find("/UI/MainMenu/Quit").SetActive(!showingOrNotShowing);
        }
    }
}