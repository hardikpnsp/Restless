using UnityEngine;

public class GroundTrigger : MonoBehaviour {

    private bool triggered;

    private void Awake()
    {
        triggered = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.tag);
        if (other.tag == "Player" && triggered == false)
        {
            triggered = true;
            FindObjectOfType<gameManager>().RenderNewGround(other.transform.position.z);

        }
    }
}
