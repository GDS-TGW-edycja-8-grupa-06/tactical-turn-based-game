using Prime31.StateKit;
using UnityEngine;

public class MovingCamera : SKState<BattleSystem>
{
    Vector3 cameraPosition;
    Vector3 pawnPosition;
    GameObject selectedPawn;

    public MovingCamera()
    {
        ;
    }

    public override void update(float deltaTime)
    {
        Camera.main.transform.position = Vector3.Lerp(cameraPosition, pawnPosition, .01f);
    }

    public override void begin()
    {
        base.begin();

        if (_context.selectedPawn != null)
        {
            _machine.changeState<ShowingContextMenu>();

            selectedPawn = _context.selectedPawn;
            cameraPosition = Camera.main.transform.position;
            pawnPosition = new Vector3(selectedPawn.transform.position.x, selectedPawn.transform.position.y, -2.5f);

            return;
        }
    }
}
