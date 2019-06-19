using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{

    public float moveSpeed;
    public float steerSpeed;

    /*
    public float maxUprightAngle;
    
    */
    public float uprightRate;

    Rigidbody rb;
    Vector3 moveValues;
    Quaternion steerValues;

    /*
    float uprightX;
    float uprightZ;
    Quaternion rotateUpright;
    */

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        moveValues = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, 0, Input.GetAxis("Vertical") * moveSpeed);

        //moveValues = new Vector3(0, 0, Input.GetAxis("Vertical") * moveSpeed);
        //steerValues = Quaternion.Euler(0, Input.GetAxis("Horizontal") * steerSpeed, 0);

        moveValues = transform.rotation * moveValues;

        /*
        if (transform.rotation.x >= maxUprightAngle)
        {
            uprightX = -uprightRate;
        }
        else if (transform.rotation.x <= -maxUprightAngle)
        {
            uprightX = uprightRate;
        }
        else
        {
            uprightX = 0;
        }

        if (transform.rotation.z >= maxUprightAngle)
        {
            uprightZ = -uprightRate;
        }
        if (transform.rotation.z <= -maxUprightAngle)
        {
            uprightZ = uprightRate;
        }
        else
        {
            uprightZ = 0;
        }

        print("X = " + uprightX + ", Z = " + uprightZ);

        //print("X = " + transform.rotation.x + ", Z = " + transform.rotation.z);

        rotateUpright = Quaternion.Euler(uprightX * Time.fixedDeltaTime, 0, uprightZ * Time.fixedDeltaTime);
        //rotateUpright = Quaternion.Euler(uprightX, 0, uprightZ);
        */

        

    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + moveValues * Time.deltaTime);

        //rb.MoveRotation(rotateUpright);

        //rb.rotation = (transform.rotation * rotateUpright);

        //rb.rotation = rotateUpright;

        //rb.rotation = steerValues * Time.fixedDeltaTime;

        //rb.AddTorque(steerValues);

        /*
        rb.MoveRotation(steerValues);

        var rot = Quaternion.FromToRotation(transform.up, Vector3.up);
        rb.AddTorque(new Vector3(rot.x, rot.y, rot.z) * uprightRate);
        */

    }
}
