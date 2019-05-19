using UnityEngine;

public class playerCollision : MonoBehaviour {

    public Rigidbody rb;

    public float sideForce;

    public playerMovement movement;

    public ParticleSystem sideParticles;

    public ParticleSystem obsParticles;

    public PowerState ps;

    [SerializeField]
    public score s;

    public float sf;

    enum States
    {
        GOING_RIGHT,
        GOING_LEFT,
        DEAD
    };

    public int state;

    void Start()
    {
        sf = sideForce;
        state = (int)States.GOING_LEFT;
        FindObjectOfType<AudioManager>().Play("Breath");
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.tag);
        if (state != (int)States.DEAD) { 
            if (collision.collider.tag == "LeftSide")
            {
                
                FindObjectOfType<AudioManager>().Play("heartbeat");
                sideParticles.transform.position = rb.transform.position;
                sideParticles.Play();
                state = (int)States.GOING_RIGHT;
            }
            else if (collision.collider.tag == "RightSide")
            {
                FindObjectOfType<AudioManager>().Play("heartbeat");
                sideParticles.transform.position = rb.transform.position;
                sideParticles.Play();
                state = (int)States.GOING_LEFT;
                
            }
            if (collision.collider.tag == "obstacle")
            {
                if (ps.powerState == (int)PowerState.PowerStates.NORMAL)
                {
                    state = (int)States.DEAD;
                    movement.enabled = false;
                    FindObjectOfType<AudioManager>().Play("Fall");
                    obsParticles.transform.position = rb.transform.position;
                    obsParticles.Play();
                    FindObjectOfType<AudioManager>().Stop("Breath");
                    FindObjectOfType<gameManager>().EndGame();
                }
                else if(ps.powerState == (int)PowerState.PowerStates.EGO)
                {
                    s.addBonusScore(1);
                    FindObjectOfType<AudioManager>().Play("Fall");
                    obsParticles.transform.position = rb.transform.position;
                    obsParticles.Play();
                    Destroy(collision.collider);
                }

            }
        }
    }

    void FixedUpdate()
    {
        if(ps.powerState == (int)PowerState.PowerStates.EGO)
        {
            sf = 10 * sideForce;
        }
        else if(ps.powerState == (int)PowerState.PowerStates.NORMAL ||
            ps.powerState == (int)PowerState.PowerStates.CALM)
        {
            sf = sideForce;
        }
        else if(ps.powerState == (int)PowerState.PowerStates.ANXIETY)
        {
            sf = (float)(0.1 * sideForce);
        }
        if (state == (int)States.GOING_LEFT)
        {
            rb.AddForce((-1) * sf * Time.deltaTime, 0, 0);
        }else if(state == (int)States.GOING_RIGHT)
        {
            rb.AddForce(sf * Time.deltaTime, 0, 0);
        }else if(state == (int)States.DEAD)
        {
            
        }


       
    }


}
