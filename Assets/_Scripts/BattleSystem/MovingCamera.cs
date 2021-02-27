using Prime31.StateKit;
using UnityEngine;

public class MovingCamera : SKState<BattleSystem>
{
    Vector3 cameraPosition;
    Vector3 moveToPosition;
    GameObject selectedPawn;

    public override void begin()
    {
        base.begin();

        cameraPosition = Camera.main.transform.position;

        if (_context.selectedUnit != null)
        {
            selectedPawn = _context.selectedUnit;
            moveToPosition = new Vector3(selectedPawn.transform.position.x, selectedPawn.transform.position.y, -2.5f);
        }
        else
        {
            moveToPosition = _context.cameraPosition;

            //_machine.changeState<Idle>();
        }
    }

    public override void update(float deltaTime)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 cameraPositon = Camera.main.transform.position;
            moveToPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float distance = Vector3.Distance(cameraPositon, moveToPosition);

            if (distance < 5.0f)
            {
                moveToPosition = cameraPosition;
            }

            moveToPosition.y = Mathf.Clamp(moveToPosition.y, -4.5f, -0.5f);
            moveToPosition.x = Mathf.Clamp(moveToPosition.x, -10f, 8f);
        }

        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, moveToPosition, 0.25f);

        if (Camera.main.transform.position == moveToPosition)
        {
            Debug.Log("Camera moved...");

            if (_context.selectedUnit != null)
            {
                _machine.changeState<ShowingContextMenu>();
            }
            else
            {
                _machine.changeState<Idle>();
            }
        }
    }
}
