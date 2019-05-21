using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planeMovement : MonoBehaviour {

    public Transform pt;
    Vector3 v = new Vector3(0, 0, 35);
    Vector3 vx;
 
	// Update is called once per frame
	void Update () {
        vx = pt.position + v;
        vx.x = 0;
        vx.y = -12;
        gameObject.transform.position = vx;
	}
}
