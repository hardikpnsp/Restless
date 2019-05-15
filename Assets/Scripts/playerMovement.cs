using UnityEngine;

public class playerMovement : MonoBehaviour {

    public Rigidbody rb;

    [SerializeField]
    public int forwardForce;

    public float sidewaysForce;
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetKey("w")) {
            rb.AddForce(0, 0, forwardForce * Time.deltaTime);
        }
        if(rb.position.y < -10)
        {
            Debug.Log("unexpected !");
            FindObjectOfType<gameManager>().EndGame();
        }

	}
}
