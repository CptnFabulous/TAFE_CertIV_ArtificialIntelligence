using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit_Old : MonoBehaviour {

    public Transform rotateAxis;

    public float distance;
    //public float xSpeed;
    //public float ySpeed;
    public Vector2 speed;
    public float yMin;
    public float yMax;
    Vector2 cameraMove;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void LateUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            Cursor.visible = false;

            cameraMove.x += Input.GetAxis("Mouse X") * speed.x * Time.deltaTime;
            cameraMove.y += Input.GetAxis("Mouse Y") * speed.y * Time.deltaTime;
            cameraMove.y = Mathf.Clamp(cameraMove.y, yMin, yMax);
        }
        else
        {
            Cursor.visible = true;
        }

        transform.rotation = Quaternion.Euler(cameraMove.y, cameraMove.x, 0);
        transform.position = rotateAxis.transform.position + (-transform.forward * distance);

        // This can be adapted for a third-person character as well! Add a spherecast behind the camera to detect if there is terrain behind it, and move the camera forward to prevent it from colliding.
    }
}
