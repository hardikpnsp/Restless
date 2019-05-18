using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemonTrigger : MonoBehaviour {

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<gameManager>().gotLemon(other.transform, true);
        }
        else
        {
            FindObjectOfType<gameManager>().gotLemon(other.transform, false);
        }
        Destroy(gameObject);
    }
}
