using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnxietyTrigger : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<AudioManager>().Play("pickup", false);
            FindObjectOfType<gameManager>().gotAnxiety(other.transform, true);
        }
        else
        {
            FindObjectOfType<gameManager>().gotAnxiety(other.transform, false);
        }
        Destroy(gameObject);
    }
}
