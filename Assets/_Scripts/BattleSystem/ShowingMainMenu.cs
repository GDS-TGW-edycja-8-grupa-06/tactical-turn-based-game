using UnityEngine;
using Prime31.StateKit;
using System.Collections;
using TMPro;

namespace Bodzio2k.BattleSystem
{
    class ShowingMainMenu : SKState<BattleSystem>
    {
        public override void update(float deltaTime)
        {
            return;
        }

        public override void begin()
        {
            base.begin();

            _context.mainMenu.gameObject.SetActive(true);

            GameObject.Find("/UI/Canvas/RoundAnnouncer").SetActive(false);
        }
    }
}
