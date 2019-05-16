using UnityEngine;

public class playerCollision : MonoBehaviour {

    public Rigidbody rb;

    public float sideForce;

    public playerMovement movement;

    public ParticleSystem sideParticles;

    public ParticleSystem obsParticles;

    enum States
    {
        GOING_RIGHT,
        GOING_LEFT,
        DEAD
    };

    public int state;  

    void Start()
    {
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
                state = (int)States.DEAD;
                movement.enabled = false;
                FindObjectOfType<AudioManager>().Play("Fall");
                FindObjectOfType<AudioManager>().Stop("Breath");
                obsParticles.transform.position = rb.transform.position;
                obsParticles.Play();
                FindObjectOfType<gameManager>().EndGame();
            }
        }
    }

    void FixedUpdate()
    {
        if (state == (int)States.GOING_LEFT)
        {
            rb.AddForce((-1) * sideForce * Time.deltaTime, 0, 0);
        }else if(state == (int)States.GOING_RIGHT)
        {
            rb.AddForce(sideForce * Time.deltaTime, 0, 0);
        }else if(state == (int)States.DEAD)
        {
            
        }
    }
}
