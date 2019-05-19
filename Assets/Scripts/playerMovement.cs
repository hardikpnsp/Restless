using UnityEngine;

public class playerMovement : MonoBehaviour {

    public Rigidbody rb;

    [SerializeField]
    public int forwardForce;

    public float ff;

    public float sidewaysForce;

    public PowerState ps; 

    public TrailRenderer trail;

    private void Awake()
    {
        trail.emitting = true;
    }
    // Update is called once per frame
    void FixedUpdate () {

        if(ps.powerState == (int)PowerState.PowerStates.EGO)
        {
            ff = forwardForce * 10;
        }else if (ps.powerState == (int)PowerState.PowerStates.NORMAL ||
            ps.powerState == (int)PowerState.PowerStates.CALM)
        {
            ff = forwardForce;
        }else if(ps.powerState == (int)PowerState.PowerStates.ANXIETY)
        {
            ff = (float)(0.1 * forwardForce);
        }

        if (Input.touchCount > 0 || Input.GetKey("w")) {
            if (trail.startWidth <= 1.2f)
            {
                trail.startWidth += 1f * Time.deltaTime;
            }
            rb.AddForce(0, 0, ff * Time.deltaTime);
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
            Debug.LogWarning("unexpected !");
            FindObjectOfType<gameManager>().EndGame();
        }

	}

}
