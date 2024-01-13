using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EgoTrigger : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<AudioManager>().Play("pickup", false);
            FindObjectOfType<gameManager>().gotEgo(other.transform, true);
        }
        else
        {
            FindObjectOfType<gameManager>().gotEgo(other.transform, false);
        }
        Destroy(gameObject);
    }

}
