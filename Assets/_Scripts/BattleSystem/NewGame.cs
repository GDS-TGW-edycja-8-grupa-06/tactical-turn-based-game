using UnityEngine;
using Prime31.StateKit;
using System.Collections;

namespace Bodzio2k.BattleSystem
{
    class NewGame : SKState<BattleSystem>
    {
        private GameObject roundAnnouncer;

        public override void update(float deltaTime)
        {
            return;
        }

        public override void begin()
        {
            base.begin();

            _context.mainMenu.gameObject.SetActive(false);
            _context.canvas.gameObject.SetActive(true);

            roundAnnouncer = GameObject.Find("/UI/Canvas/RoundAnnouncer");
            roundAnnouncer.SetActive(true);

            _context.StartCoroutine(HideRoundAnnouncer());

            _context.sm.changeState<Idle>();
        }

        public override void end()
        {
            base.end();
        }

        IEnumerator HideRoundAnnouncer()
        {
            yield return new WaitForSeconds(2f);

            roundAnnouncer.SetActive(false);
        }
    }
}