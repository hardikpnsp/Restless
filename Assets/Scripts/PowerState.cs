using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerState : MonoBehaviour {

    public Transform playerTransform;

    public Rigidbody rb;
    public enum PowerStates
    {
        EGO,
        ANXIETY,
        NORMAL,
        CALM
    };

    public int powerState;

    private void Start()
    {
        powerState = (int)PowerStates.NORMAL;
    }

    private void FixedUpdate()
    {
        if (powerState == (int)PowerStates.EGO)
        {
            rb.mass = 10;
            float increase = (float)(1 * Time.deltaTime);
            if (playerTransform.localScale.x <= 4)
            {
                playerTransform.localScale += new Vector3(increase, increase, increase);
            }
        }else if(powerState == (int)PowerStates.NORMAL)
        {
            rb.mass = 1;
            float decrease = (float)(10 * Time.deltaTime);
            if (playerTransform.localScale.x > 1)
            {
                playerTransform.localScale -= new Vector3(decrease, decrease, decrease);
            }
            else if(playerTransform.localScale.x < 1)
            {
                playerTransform.localScale += new Vector3(decrease, decrease, decrease);
            }
            if((playerTransform.localScale.x > 0.9 && playerTransform.localScale.x < 1) || 
                (playerTransform.localScale.x < 1.1 && playerTransform.localScale.x > 1))
            {
                playerTransform.localScale = new Vector3(1, 1, 1);
            }
        }else if(powerState == (int)PowerStates.ANXIETY)
        {
            rb.mass = 0.1f;
            Debug.Log("entered state anxiety");
        }
    }

    public void SetPowerState(int i)
    {
        powerState = i;
    }
}
