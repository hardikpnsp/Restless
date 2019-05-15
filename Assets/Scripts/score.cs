using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour {

    public Text scoreText; 
    public Transform player;

	// Update is called once per frame
	void Update () {
        if(player.position.z >= 0)
        {
            scoreText.text = ((int)player.position.z / 20).ToString();
        }
	}
}
