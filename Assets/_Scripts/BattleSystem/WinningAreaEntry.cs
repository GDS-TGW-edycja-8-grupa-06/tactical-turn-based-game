namespace Bodzio2k.BattleSystem
{
    public class WinningAreaEntry
    {
        public Unit.Unit unit;
        public Side side;
        public int roundEntered;

        public WinningAreaEntry(Unit.Unit unit, Side side, int round)
        {
            this.unit = unit;
            this.side = side;
            this.roundEntered = round;
        }
    }
}
