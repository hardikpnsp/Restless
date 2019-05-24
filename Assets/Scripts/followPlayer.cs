using UnityEngine;

public class followPlayer : MonoBehaviour {
	
    public Transform player;
    public PowerState ps;
    public Vector3 offset;
    public Vector3 anxietyOffset;
    public float cameraDamp;
    float cd;
    public Vector3 calcOffset;
    // Update is called once per frame
    public void Start()
    {
        cd = 0.1f;
        calcOffset = offset;
        transform.position = player.position + offset;
    }
    public void Update () {
        if(ps.powerState == (int)PowerState.PowerStates.ANXIETY)
        {
            if(calcOffset.y > anxietyOffset.y || calcOffset.z < anxietyOffset.z)
            {
                calcOffset = calcOffset + new Vector3(0, (-4.5f) * Time.deltaTime, (3f) * Time.deltaTime);
            }
            if(cd < 0.6)
            {
                cd += (float)(0.6 * Time.deltaTime);
            }
            if (cd < 0.67 && cd > 0.53)
            {
                cd = 0.6f;
            }
            Vector3 v = player.position + calcOffset;
            v.x = (float)(player.position.x * cd);
            transform.position = v;
        }
        else
        {
            if (calcOffset.y < offset.y || calcOffset.z > offset.z)
            {
                calcOffset = calcOffset - new Vector3(0, (-4.5f) * Time.deltaTime, (3f) * Time.deltaTime);
            }
            if(cd > cameraDamp)
            {
                cd -= (float)(0.3 * Time.deltaTime);
            }
            if (cd >= cameraDamp - 0.07f && cd <= cameraDamp + 0.07f)
            {
                cd = cameraDamp;
            }
            Vector3 v = player.position + calcOffset;
            v.x = (float)(player.position.x * cd);
            transform.position = v;
        }
	}
}
