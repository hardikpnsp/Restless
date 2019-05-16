using UnityEngine;

public class playerMovement : MonoBehaviour {

    public Rigidbody rb;

    [SerializeField]
    public int forwardForce;

    public float sidewaysForce;

    public TrailRenderer trail;

    private void Awake()
    {
        trail.emitting = true;
    }
    // Update is called once per frame
    void FixedUpdate () {
        if (Input.touchCount > 0 || Input.GetKey("w")) {
            if (trail.startWidth <= 1.2f)
            {
                trail.startWidth += 1f * Time.deltaTime;
            }
            rb.AddForce(0, 0, forwardForce * Time.deltaTime);
        }
        else
        {
            if (trail.startWidth >= 0.2f)
            {
                trail.startWidth -= 1f * Time.deltaTime;
            }
        }
        if(rb.position.y < -10)
        {
            Debug.Log("unexpected !");
            FindObjectOfType<gameManager>().EndGame();
        }

	}

}
