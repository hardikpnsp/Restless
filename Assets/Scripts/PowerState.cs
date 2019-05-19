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
        CALM,
        TRANSITIONEGO,
        TRANSITIONANX,
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
            float increase = (float)(4 * Time.deltaTime);
            if (playerTransform.localScale.x <= 4)
            {
                playerTransform.localScale += new Vector3(increase, increase, increase);
            }
        }else if(powerState == (int)PowerStates.TRANSITIONEGO)
        {
            if (playerTransform.localScale.x > 1)
            {
                float decrease = (float)(1 * Time.deltaTime);
                playerTransform.localScale -= new Vector3(decrease, decrease, decrease);
            }
            else if(playerTransform.localScale.x < 1)
            {
                float increase = (float)(0.5 * Time.deltaTime);
                playerTransform.localScale += new Vector3(increase, increase, increase);
            }
            if((playerTransform.localScale.x > 0.9 && playerTransform.localScale.x < 1) || 
                (playerTransform.localScale.x < 1.1 && playerTransform.localScale.x > 1))
            {
                playerTransform.localScale = new Vector3(1, 1, 1);
                powerState = (int)PowerStates.NORMAL;
            }
        }else if(powerState == (int)PowerStates.ANXIETY)
        {
            float decrease = (float)(2 * Time.deltaTime);
            if (playerTransform.localScale.x >= 0.4)
            {
                playerTransform.localScale -= new Vector3(decrease, decrease, decrease);
            }
        }else if(powerState == (int)PowerStates.TRANSITIONANX)
        {
            if (playerTransform.localScale.x > 1)
            {
                float decrease = (float)(1 * Time.deltaTime);
                playerTransform.localScale -= new Vector3(decrease, decrease, decrease);
            }
            else if (playerTransform.localScale.x < 1)
            {
                float increase = (float)(0.3 * Time.deltaTime);
                playerTransform.localScale += new Vector3(increase, increase, increase);
            }
            if ((playerTransform.localScale.x > 0.9 && playerTransform.localScale.x < 1) ||
                (playerTransform.localScale.x < 1.1 && playerTransform.localScale.x > 1))
            {
                playerTransform.localScale = new Vector3(1, 1, 1);
                powerState = (int)PowerStates.NORMAL;
            }
        }
    }

    public void SetPowerState(int i)
    {
        powerState = i;
    }

    public void StateChange()
    {
        if (powerState == (int)PowerStates.EGO)
        {
            rb.mass = 10;
        }
        else if (powerState == (int)PowerStates.NORMAL ||
            powerState == (int)PowerState.PowerStates.TRANSITIONANX ||
            powerState == (int)PowerState.PowerStates.TRANSITIONEGO ||
            powerState == (int)PowerState.PowerStates.TRANSITIONANX )
        {
            rb.mass = 1;
        }
        else if (powerState == (int)PowerStates.ANXIETY)
        {
            rb.mass = 0.2f;
        }
    }
}
