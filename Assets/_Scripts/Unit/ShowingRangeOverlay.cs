using UnityEngine;
using Prime31.StateKit;

public class ShowingRangeOverlay : SKState<Unit>
{
    private int moveRange;
    
    public override void begin()
    {
        base.begin();

        _context.CreateRangeOverlay(OverlayType.Move);
    }

    public override void end()
    {
        base.end();

        _context.HideRangeOverlay();
    }

    public override void update(float deltaTime)
    {
        return;
    }
}
