using UnityEngine;

public class followPlayer : MonoBehaviour {
	
    public Transform player;
    public PowerState ps;
    public Camera c;
    [SerializeField]
    public Vector3 offset;
    public Vector3 anxietyOffset;
    public float cameraDamp;
    float cd;
    public Vector3 calcOffset;
    public int anxietySize;
    public int normalSize;


    public Vector3 rotationSet;

    public Vector3 cameraRotate;

    public Vector3 rotationSpeed;

    //implementation incomplete
    public enum CameraState
    {
        SIMPLE, 
        FLIP,
        DEGREE_45
    };


    public void Start()
    {
        cd = 0.1f;
        calcOffset = offset;
        transform.position = player.position + offset;
    }


    public void Update () {

        cameraRotate.x = transform.eulerAngles.x;
        cameraRotate.y = transform.eulerAngles.y;
        cameraRotate.z = transform.eulerAngles.z;

        //for y
        if (cameraRotate.y > rotationSet.y - 0.5f && cameraRotate.y < rotationSet.y + 0.5f)
        {
            cameraRotate.y = rotationSet.y;
        }
        if (cameraRotate.y < rotationSet.y)
        {
            cameraRotate.y += rotationSpeed.y * Time.deltaTime;
        }
        else if (cameraRotate.y > rotationSet.y)
        {
            cameraRotate.y -= rotationSpeed.y * Time.deltaTime;
        }


        //for z
        if (cameraRotate.z > rotationSet.z - 0.5f && cameraRotate.z < rotationSet.z + 0.5f)
        {
            cameraRotate.z = rotationSet.z;
        }
        if (cameraRotate.z > rotationSet.z)
        {
            cameraRotate.z -= rotationSpeed.z * Time.deltaTime;

        }
        else if (cameraRotate.z < rotationSet.z)
        {
            cameraRotate.z += rotationSpeed.z * Time.deltaTime;

        }


        //for x
        if (cameraRotate.x > rotationSet.x - 0.5f && cameraRotate.x < rotationSet.x + 0.5f)
        {
            cameraRotate.x = rotationSet.x;
        }
        if (cameraRotate.x > rotationSet.x)
        {
            cameraRotate.x -= rotationSpeed.x * Time.deltaTime;
        }
        else if (cameraRotate.x < rotationSet.x)
        {
            cameraRotate.x += rotationSpeed.x * Time.deltaTime;
        }

        //make rotation
        transform.eulerAngles = cameraRotate;


        if (ps.powerState == (int)PowerState.PowerStates.ANXIETY)
        {
            //handle orthographic camera size
            if (c.orthographicSize < anxietySize + 0.2f && c.orthographicSize > anxietySize - 0.2f)
            {
                c.orthographicSize = anxietySize;
            }
            if (c.orthographicSize < anxietySize)
            {
                c.orthographicSize += (normalSize - anxietySize) * 0.4f * Time.deltaTime;
            }
            else if (c.orthographicSize > anxietySize)
            {
                c.orthographicSize -= (normalSize - anxietySize) * 0.4f * Time.deltaTime;
            }


            //handle anxiety offset
            /*
            if (calcOffset.z > anxietyOffset.z)
            {
                calcOffset = calcOffset + new Vector3(0, 0, (-1) * (3f) * Time.deltaTime);
            }
            */

            //for y
            if (calcOffset.y > anxietyOffset.y - 0.2f && calcOffset.y < anxietyOffset.y + 0.2f)
            {
                calcOffset.y = anxietyOffset.y;
            }
            if (calcOffset.y < anxietyOffset.y)
            {
                calcOffset.y += (4.5f) * Time.deltaTime;

            }
            else if (calcOffset.y > anxietyOffset.y)
            {
                calcOffset.y -= (4.5f) * Time.deltaTime;

            }


            //for z
            if (calcOffset.z > anxietyOffset.z - 0.2f && calcOffset.z < anxietyOffset.z + 0.2f)
            {
                calcOffset.z = anxietyOffset.z;
            }
            if (calcOffset.z > anxietyOffset.z)
            {
                calcOffset.z -= (4f) * Time.deltaTime;

            }
            else if (calcOffset.z < anxietyOffset.z)
            {
                calcOffset.z += (4f) * Time.deltaTime;

            }


            //for x
            if (calcOffset.x > anxietyOffset.x - 0.2f && calcOffset.x < anxietyOffset.x + 0.2f)
            {
                calcOffset.x = anxietyOffset.x;
            }
            if (calcOffset.x > anxietyOffset.x)
            {
                calcOffset.x -= (10f) * Time.deltaTime;

            }
            else if (calcOffset.x < anxietyOffset.x)
            {
                calcOffset.x += (10f) * Time.deltaTime;

            }



            //handle camera damp for now set to 0.6
            if (cd < 0.67 && cd > 0.53)
            {
                cd = 0.6f;
            }
            if (cd < 0.6)
            {
                cd += (float)(0.6 * Time.deltaTime);
            }else if(cd > 0.6)
            {
                cd -= (float)(0.6 * Time.deltaTime);
            }


            Vector3 v = player.position + calcOffset;
            v.x = (float)(player.position.x * cd) + anxietyOffset.x;
            transform.position = v;
        }
        else
        {

            //handle orthographic camera size

            if (c.orthographicSize < normalSize + 0.2f && c.orthographicSize > normalSize - 0.2f)
            {
                c.orthographicSize = normalSize;
            }
            if (c.orthographicSize < normalSize)
            {
                c.orthographicSize += (normalSize - anxietySize) * 0.4f * Time.deltaTime;
            }
            else if(c.orthographicSize > normalSize)
            {
                c.orthographicSize -= (normalSize - anxietySize) * 0.4f * Time.deltaTime;
            }



            //handle offset

            //for y
            if (calcOffset.y > offset.y - 0.2f && calcOffset.y < offset.y + 0.2f)
            {
                calcOffset.y = offset.y;
            }
            if (calcOffset.y < offset.y)
            {
                calcOffset.y += (4.5f) * Time.deltaTime;

            }else if(calcOffset.y > offset.y)
            {
                calcOffset.y -= (4.5f) * Time.deltaTime;

            }


            //for z
            if (calcOffset.z > offset.z - 0.5f && calcOffset.z < offset.z + 0.5f)
            {
                calcOffset.z = offset.z;
            }
            if (calcOffset.z > offset.z)
            {
                calcOffset.z -= (2f) * Time.deltaTime;
            }
            else if(calcOffset.z < offset.z)
            {
                calcOffset.z += (2f) * Time.deltaTime;
            }


            //for x
            if (calcOffset.x > offset.x - 0.2f && calcOffset.x < offset.x + 0.2f)
            {
                calcOffset.x = offset.x;
            }
            if (calcOffset.x > offset.x)
            {
                calcOffset.x -= (10f) * Time.deltaTime;

            }
            else if (calcOffset.x < offset.x)
            {
                calcOffset.x += (10f) * Time.deltaTime;

            }



            //handle camera damp 

            if (cd >= cameraDamp - 0.07f && cd <= cameraDamp + 0.07f)
            {
                cd = cameraDamp;
            }
            if (cd > cameraDamp)
            {
                cd -= (float)(0.3 * Time.deltaTime);
            }else if(cd < cameraDamp)
            {
                cd += (float)(0.3 * Time.deltaTime);
            }


            Vector3 v = player.position + calcOffset;
            v.x = (float)(player.position.x * cd) + calcOffset.x;
            transform.position = v;
        }
	}
}
