using UnityEngine;
using Prime31.StateKit;

namespace Bodzio2k.BattleSystem
{
    class NewGame : SKState<BattleSystem>
    {
        public override void update(float deltaTime)
        {
            return;
        }

        public override void begin()
        {
            base.begin();

            _context.mainMenu.gameObject.SetActive(false);
            _context.canvas.gameObject.SetActive(true);

        }

        public override void end()
        {
            base.end();
        }
    }
}