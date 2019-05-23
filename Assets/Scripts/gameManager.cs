using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class gameManager : MonoBehaviour {

    [SerializeField]
    public GameObject[] groundPrefabs;
    public int extraQuotes;
    private int[,] quoteSpawn; 

    public GameObject cameraX;

    [HideInInspector]
    public int groundNumber;


    private GameObject current;
    private GameObject buffer0;
    private GameObject buffer2;

    [SerializeField]
    public ParticleSystem lemonParticles;
    public ParticleSystem egoParticles;
    public playerMovement movement;

    public PowerState ps; 

    public LemonScore lemons;

    bool gameHasEnded = false;
    public float delay;

    public GameObject deathUI;
    public GameObject startUI;

    public LemonScore endGameLemonScore;

    public score s;

    public playerCollision pc;

    int highScore;
    int totalLemons;

    public QuoteManager qm;

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        totalLemons = PlayerPrefs.GetInt("Lemons", 0);
        startUI.GetComponentInChildren<HighScore>().SetHighScore("highscore: " + highScore);
        startUI.GetComponentInChildren<TotalLemon>().SetTotalLemon("Lemons: " + totalLemons);

        qm.SetUpQuoteManager(groundPrefabs.Length, extraQuotes);

        //QuoteManager in- groundPrefabs.Length, extraQuotes
        /*
        quoteSpawn = new int[groundPrefabs.Length,extraQuotes];
        int i = 0;
        int j = 0;
        for(i=0; i<groundPrefabs.Length; i++)
        {
            for(j=0; j<extraQuotes; j++)
            {
                quoteSpawn[i,j] = 1;
            }
        }
        */
        movement.enabled = false;

        current = Instantiate(groundPrefabs[0], new Vector3(0, 0, 0), Quaternion.identity);

        //QuoteManager in- current type: gameObject
        qm.SetQuote(current, 0);
        //comment this 
        //current.GetComponentInChildren<SetText>().setText(Random.Range(0, 8));
        //comment this end
        groundNumber = 0;

 
    }

    public void EndGame()
    {
        if (gameHasEnded == false)
        {

            gameHasEnded = true;
            //Debug.Log("Game Over");
            cameraX.GetComponent<followPlayer>().enabled = false;

            Invoke("PlayEndAnimation", 2);
        }
    }

    public void StartGame()
    {
        movement.enabled = true;
        ps.StateChange();
        pc.StateChange();
        movement.StateChange();
        startUI.SetActive(false);
    }

    private void PlayEndAnimation()
    {
        deathUI.SetActive(true);
        deathUI.GetComponentInChildren<score>().setScore(s.getScore());
        endGameLemonScore.SetLemons(lemons.GetLemons());
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (highScore <= s.getScore())
        {
            PlayerPrefs.SetInt("HighScore", s.getScore());
            startUI.GetComponentInChildren<HighScore>().SetHighScore("HigheScore: " + highScore);
        };
        totalLemons = PlayerPrefs.GetInt("Lemons", 0);
        PlayerPrefs.SetInt("Lemons", totalLemons + lemons.GetLemons());
    }


    public void RenderNewGround(float z)
    {
        if (z < 0)
        {
            z = 1;
            int r = Random.Range(0, groundPrefabs.Length);
            //Debug.Log("r: " + r);
            //Debug.Log("z: " + z);
            buffer2 = Instantiate(groundPrefabs[r], new Vector3(0, 0, z * 100), Quaternion.identity);

            //QuoteManager
            qm.SetQuoteWithProbability(buffer2, r);
            /*
            int quoteNumber = Random.Range(0, extraQuotes);

            while (quoteSpawn[r, quoteNumber] != 1)
            {
                quoteNumber = Random.Range(0, extraQuotes);
            }
            quoteSpawn[r, quoteNumber] = 0;
            buffer2.GetComponentInChildren<SetText>().setText(quoteNumber);
            */
        }
        else {
            z = (int)(z / 100);
            z = z + 2;
            int r = Random.Range(1, groundPrefabs.Length);
            //Debug.Log("r: " + r);
            //Debug.Log("z: " + z);
            buffer0 = current;
            current = buffer2;
            buffer2 = Instantiate(groundPrefabs[r], new Vector3(0, 0, z * 100), Quaternion.identity);
            //QuoteManager
            qm.SetQuoteWithProbability(buffer2, r);
            /*
            int quoteNumber = Random.Range(0, extraQuotes);
            
            while(quoteSpawn[r,quoteNumber] != 1)
            {
                quoteNumber = Random.Range(0, extraQuotes);
            }
            quoteSpawn[r, quoteNumber] = 0;
            buffer2.GetComponentInChildren<SetText>().setText(quoteNumber);
            */
            Destroy(buffer0);
        
        }
        //current.GetComponent<GroundTrigger>().enabled = false;
        groundNumber++;
        //Debug.Log("Ground: " + groundNumber);
    }

    public void Restart()
    {
 
        SceneManager.LoadScene("MainScene");

    }

    public void gotLemon(Transform t, bool byPlayer)
    {
        lemonParticles.transform.position = t.position;
        lemonParticles.Play();
        if (byPlayer)
        {
            lemons.IncreaseLemons();
        }
    }

    public void gotEgo(Transform t, bool byPlayer)
    {
        if (byPlayer)
        {
            if (ps.powerState == (int)PowerState.PowerStates.NORMAL ||
            ps.powerState == (int)PowerState.PowerStates.CALM ||
            ps.powerState == (int)PowerState.PowerStates.TRANSITIONANX ||
            ps.powerState == (int)PowerState.PowerStates.TRANSITIONEGO)
            {
                ps.SetPowerState((int)PowerState.PowerStates.EGO);
                ps.StateChange();
                pc.StateChange();
                movement.StateChange();
                Invoke("goNormal", 10);
            }
        }
    }

    public void goNormal()
    {
        if (ps.powerState == (int)PowerState.PowerStates.EGO)
        {
            ps.SetPowerState((int)PowerState.PowerStates.TRANSITIONEGO);
        }else if(ps.powerState == (int)PowerState.PowerStates.ANXIETY)
        {
            ps.SetPowerState((int)PowerState.PowerStates.TRANSITIONANX);
        }
        ps.StateChange();
        pc.StateChange();
        movement.StateChange();
    }

    public void gotAnxiety(Transform t, bool byPlayer)
    {
        if (byPlayer)
        {
            if (ps.powerState == (int)PowerState.PowerStates.NORMAL ||
            ps.powerState == (int)PowerState.PowerStates.CALM ||
            ps.powerState == (int)PowerState.PowerStates.TRANSITIONANX ||
            ps.powerState == (int)PowerState.PowerStates.TRANSITIONEGO)
            {
                ps.SetPowerState((int)PowerState.PowerStates.ANXIETY);
                ps.StateChange();
                pc.StateChange();
                movement.StateChange();
                Invoke("goNormal", 20);
            }
        }
    }

    public void OpenSecret()
    {
        totalLemons = PlayerPrefs.GetInt("Lemons", 0);
        if (totalLemons >= 500)
        {
            Debug.Log("unleashing the secret");
        }
        else
        {
            startUI.GetComponentInChildren<SecretMessage>().SetText("You need 500 lemons for that");
        }
    }
}
