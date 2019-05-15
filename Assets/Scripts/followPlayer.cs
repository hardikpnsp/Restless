using UnityEngine;

public class followPlayer : MonoBehaviour {
	
    public Transform player;
    public Vector3 offset;
    public float cameraDamp;
	// Update is called once per frame
	public void Update () {
        Vector3 v = player.position + offset;
        v.x = (float)((player.position.x * cameraDamp) - 1.4);
        transform.position = v;
	}
}
