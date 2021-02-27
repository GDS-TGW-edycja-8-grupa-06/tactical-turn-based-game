using UnityEngine;
using Prime31.StateKit;

public class ShowingRangeOverlay : SKState<Unit>
{
    private int moveRange;
    
    public override void begin()
    {
        base.begin();

        CreateRangeOverlay();
    }

    public override void end()
    {
        base.end();
    }

    public override void update(float deltaTime)
    {
        return;
    }

    private void CreateRangeOverlay()
    {
        ;
    }
}
