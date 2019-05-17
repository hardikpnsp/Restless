using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SpawnLemmon : MonoBehaviour {

    public GameObject lemonPrefab;
    // Use this for initialization
    void Start () {

        int r = Random.Range(0, 10);
        //Debug.Log("spawning lemon");
        if (r > 7)
        {
            Instantiate(lemonPrefab, gameObject.transform);
        }
    }
	
}
