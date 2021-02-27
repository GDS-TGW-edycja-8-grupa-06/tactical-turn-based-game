using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private Vector2 moveToPosition;

    [SerializeField]
    float cameraMovementSpeed = 2.125f;

    void Start()
    {
        moveToPosition = transform.position;
    }

    void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 mousePosition3D = new Vector3(mousePosition.x, mousePosition.y, -2.5f);

            moveToPosition = Vector3.Lerp(transform.position, mousePosition3D, cameraMovementSpeed);


            transform.position = mousePosition3D;

            return;
        }
    }

    private void FixedUpdate()
    {
        
    }
}
