using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SpawnLemmon : MonoBehaviour {

    public GameObject ego;
    public GameObject anxiety;
    public int egoOrAnxiety; 
    //1 = ego only, 2 = anxiety only, 0 = random
    public int chance = 7; 
    // Use this for initialization
    void Start () {
        if (egoOrAnxiety == 1)
        {
            int r1 = Random.Range(0, 10);
            if (r1 > chance)
            {
                Instantiate(ego, gameObject.transform);
            }
        }
        else if (egoOrAnxiety == 2)
        {
            int r2 = Random.Range(0, 10);
            if (r2 > chance)
            {
                Instantiate(anxiety, gameObject.transform);
            }
        }
        else if (egoOrAnxiety == 0)
        {
            int r1 = Random.Range(0, 10);
            int r2 = Random.Range(0, 10);

            if (r2 > chance)
            {
                if (r1 >= 5)
                {
                    Instantiate(ego, gameObject.transform);
                }
                else
                {
                    Instantiate(anxiety, gameObject.transform);
                }
            }
        }
    }
}
