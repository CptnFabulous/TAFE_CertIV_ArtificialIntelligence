/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable : MonoBehaviour {

    public bool isOccupied;
}
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable : MonoBehaviour
{

    public bool isAvailable;
}

/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable : MonoBehaviour
{
    public bool isAvailable = true;
    public Transform pivotPoint;
    // Use this for initialization
    public Vector3 GetPivotPoint()
    {
        if (pivotPoint == null)
            return transform.position;

        return pivotPoint.position;
    }




}
*/
