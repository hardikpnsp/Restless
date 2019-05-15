using UnityEngine;
using UnityEngine.SceneManagement; 

public class gameManager : MonoBehaviour {

   

    [SerializeField]
    public GameObject[] groundPrefabs;
    public GameObject cameraX;
    public int groundNumber; 
    public GameObject current;
    public GameObject buffer0;
    public GameObject buffer2;

    bool gameHasEnded = false;
    public float delay;

    public GameObject deathUI;

    private void Start()
    {
        current = Instantiate(groundPrefabs[0], new Vector3(0, 0, 0), Quaternion.identity);
        groundNumber = 0;
    }

    public void EndGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("Game Over");
            cameraX.GetComponent<followPlayer>().enabled = false;
            Invoke("PlayEndAnimation", 2);
        }
    }

    private void PlayEndAnimation()
    {
        deathUI.SetActive(true);
    }
    public void RenderNewGround(float z)
    {
        Debug.Log(z);
        if (z < 0)
        {
            z = 1;
            int r = Random.Range(0, groundPrefabs.Length);
            //Debug.Log("r: " + r);
            //Debug.Log("z: " + z);
            buffer2 = Instantiate(groundPrefabs[r], new Vector3(0, 0, z * 100), Quaternion.identity);
        }
        else {
            z = (int)(z / 100);
            z = z + 2;
            int r = Random.Range(0, groundPrefabs.Length);
            //Debug.Log("r: " + r);
            //Debug.Log("z: " + z);
            buffer0 = current;
            current = buffer2;
            buffer2 = Instantiate(groundPrefabs[r], new Vector3(0, 0, z * 100), Quaternion.identity);
            Destroy(buffer0);
        }
        groundNumber++;
        Debug.Log("Ground: " + groundNumber);
    }

    public void Restart()
    {
        SceneManager.LoadScene("MainScene");
    }

}
