using UnityEngine;
using Prime31.StateKit;

public class ShowingRangeOverlay : SKState<Unit>
{
    private int moveRange;
    
    public override void begin()
    {
        base.begin();

        CreateRangeOverlay(OverlayType.Move);
    }

    public override void end()
    {
        base.end();
    }

    public override void update(float deltaTime)
    {
        return;
    }

    private void CreateRangeOverlay(OverlayType overlayType)
    {
        int range = overlayType == OverlayType.Move ? _context.unitProperties.moveRange : _context.unitProperties.attackRange;
        GameObject overlay = _context.transform.Find("Overlay").gameObject;

        if (overlay != null)
        {
            return;
        }

        Vector3 position = new Vector3(_context.transform.position.x + 1, _context.transform.position.y, _context.transform.position.z);

        //GameObject overlayTile = _context.Instantiate(_context.overlayTilePrefab, position, Quaternion.identity, overlay.transform);
    }
}
