using UnityEngine;

public class GroundTrigger : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.tag);
        if (other.tag == "Player")
        {
            FindObjectOfType<gameManager>().RenderNewGround(other.transform.position.z);
        }
    }
}
