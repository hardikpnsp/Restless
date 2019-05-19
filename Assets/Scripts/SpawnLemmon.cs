using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SpawnLemmon : MonoBehaviour {

    public GameObject lemonPrefab;
    public int chance = 7;
    // Use this for initialization
    void Start () {

        int r = Random.Range(0, 10);
        //Debug.Log("spawning lemon");
        if (r > chance)
        {
            Instantiate(lemonPrefab, gameObject.transform);
        }
    }
	
}
