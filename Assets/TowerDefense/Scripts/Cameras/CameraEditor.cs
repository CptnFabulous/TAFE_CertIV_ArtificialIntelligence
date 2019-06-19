using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEditor : MonoBehaviour
{
    public float zoomSensitivity;
    public float defaultZoomSpeed;
    float distance;
    //public float xSpeed;
    //public float ySpeed;

    /*
    [Range(-10, 10)]
    public Vector2 test;
    */

    public Vector2 sensitivity;
    public float yMin;
    public float yMax;
    Vector2 cameraRotate;

    Ray cameraZoom;
    RaycastHit zoomData;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        /*
        if (Physics.Raycast(transform.position, transform.forward, out zoomData, 999999999))
        {
            distance = zoomData.distance;
        }
        else
        {
            distance = defaultZoomSpeed;
        }

        float zoomSpeed = Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity * distance;
        */

        // Implement a 'reference' object to zoom in on

        if (Input.GetMouseButton(1))
        {
            Cursor.visible = false;

            cameraRotate.x += Input.GetAxis("Mouse X") * sensitivity.x * Time.deltaTime;
            cameraRotate.y += Input.GetAxis("Mouse Y") * sensitivity.y * Time.deltaTime;
            cameraRotate.y = Mathf.Clamp(cameraRotate.y, yMin, yMax);
            //transform.rotation = Quaternion.Euler(cameraRotate.y, cameraRotate.x, 0);
        }
        else
        {
            Cursor.visible = true;
        }

        Vector2 movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movementInput.Normalize();
        Vector3 movementValue = new Vector3(movementInput.x * Time.deltaTime, movementInput.y * Time.deltaTime, 0);

        transform.rotation = Quaternion.Euler(cameraRotate.y, cameraRotate.x, 0);
        movementValue = transform.rotation * movementValue;
        transform.Translate(movementValue);

        //transform.rotation = Quaternion.Euler(cameraRotate.y, cameraRotate.x, 0);
        //transform.position = rotateAxis.transform.position + (-transform.forward * distance);

        // This can be adapted for a third-person character as well! Add a spherecast behind the camera to detect if there is terrain behind it, and move the camera forward to prevent it from colliding.
    }
}
