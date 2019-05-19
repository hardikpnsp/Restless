using UnityEngine;

public class playerMovement : MonoBehaviour {

    public Rigidbody rb;

    [SerializeField]
    public int forwardForce;

    [SerializeField]
    public float ff;

    public PowerState ps; 

    public TrailRenderer trail;

    public float minTrailWidth;

    public float maxTrailWidth;

    private void Awake()
    {
        trail.emitting = true;
    }
    // Update is called once per frame
    void FixedUpdate () {

        
        if(trail.startWidth < minTrailWidth)
        {
            trail.startWidth += 1f * Time.deltaTime;
        }
        else if (trail.startWidth > maxTrailWidth)
        {
            trail.startWidth -= 1f * Time.deltaTime;
        }
        if (Input.touchCount > 0 || Input.GetKey("w")) {
            if (trail.startWidth <= maxTrailWidth)
            {
                trail.startWidth += 1f * Time.deltaTime;
            }
            rb.AddForce(0, 0, ff * Time.deltaTime);
        }
        else
        {
            if (trail.startWidth >= minTrailWidth)
            {
                trail.startWidth -= 1f * Time.deltaTime;
            }
        }
 
        if (rb.position.y < -50)
        {
            Debug.LogWarning("unexpected !");
            FindObjectOfType<gameManager>().EndGame();
        }

	}

    public void StateChange()
    {
        if (ps.powerState == (int)PowerState.PowerStates.EGO)
        {
            ff = forwardForce * 10;
            minTrailWidth = 1.2f;
            maxTrailWidth = 2f;
            rb.drag = 1;
        }
        else if (ps.powerState == (int)PowerState.PowerStates.NORMAL ||
            ps.powerState == (int)PowerState.PowerStates.CALM ||
            ps.powerState == (int)PowerState.PowerStates.TRANSITIONANX ||
            ps.powerState == (int)PowerState.PowerStates.TRANSITIONEGO )
        {
            minTrailWidth = 0.2f;
            maxTrailWidth = 1.2f;
            ff = forwardForce;
            rb.drag = 1;
        }
        else if (ps.powerState == (int)PowerState.PowerStates.ANXIETY)
        {
            minTrailWidth = 0.02f;
            maxTrailWidth = 0.12f;
            ff = (float)(0.2 * forwardForce);
            rb.drag = 1.3f;
        }
    }

}
