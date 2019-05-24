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

    public SecretUnlocked secret;

    public bool GoToOblivion;

    public GameObject[] OblivionPrefabs;

    public int oblivionCount = 0;

    public GameObject ScoreUI;
    public GameObject LemonUI;
    private void Start()
    {

        highScore = PlayerPrefs.GetInt("HighScore", 0);
        totalLemons = PlayerPrefs.GetInt("Lemons", 0);
        startUI.GetComponentInChildren<HighScore>().SetHighScore("highscore: " + highScore);
        startUI.GetComponentInChildren<TotalLemon>().SetTotalLemon("Lemons: " + totalLemons);

        if (secret.GetSecret())
        {
            Debug.Log("Going to the oblivion");
            GoToOblivion = true;
            startUI.GetComponentInChildren<SecretMessage>().SetText("Welcome to the oblivion, Hit Play!");
            current = Instantiate(OblivionPrefabs[0], new Vector3(0, 0, 0), Quaternion.identity);
            qm.SetOblivionQuotes(current);
            ScoreUI.SetActive(false);
            LemonUI.SetActive(false);
            pc.state = (int)playerCollision.States.CALM;
            oblivionCount++;
        }
        else
        {
            pc.state = (int)playerCollision.States.GOING_LEFT;
            current = Instantiate(groundPrefabs[0], new Vector3(0, 0, 0), Quaternion.identity);
            qm.SetQuote(current, 0);
        }

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

        

        //QuoteManager in- current type: gameObject

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
            if (GoToOblivion)
            {
                secret.SetSecretUnlocked(false);
            }
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
        if (!GoToOblivion)
        {
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
        else
        {
            deathUI.GetComponentInChildren<score>().setScore(42);
            endGameLemonScore.SetLemons(42);
            totalLemons = PlayerPrefs.GetInt("Lemons", 0);
            PlayerPrefs.SetInt("Lemons", totalLemons + lemons.GetLemons());
        }
    }


    public void RenderNewGround(float z)
    {
        if (GoToOblivion)
        {
            if (oblivionCount < 16)
            {
                if (z < 0)
                {
                    z = 1;
                    //Debug.Log("r: " + r);
                    //Debug.Log("z: " + z);
                    buffer2 = Instantiate(OblivionPrefabs[0], new Vector3(0, 0, z * 100), Quaternion.identity);
                    oblivionCount++;
                    //QuoteManager
                    qm.SetOblivionQuotes(buffer2);
                }
                else
                {
                    z = (int)(z / 100);
                    z = z + 2;
                    //Debug.Log("r: " + r);
                    //Debug.Log("z: " + z);
                    buffer0 = current;
                    current = buffer2;
                    buffer2 = Instantiate(OblivionPrefabs[0], new Vector3(0, 0, z * 100), Quaternion.identity);
                    oblivionCount++;
                    //QuoteManager
                    qm.SetOblivionQuotes(buffer2);

                    Destroy(buffer0);

                }
                //current.GetComponent<GroundTrigger>().enabled = false;
                groundNumber++;
                //Debug.Log("Ground: " + groundNumber);
            }
            else
            {
                z = (int)(z / 100);
                z = z + 2;
                buffer0 = current;
                current = buffer2;
                buffer2 = Instantiate(OblivionPrefabs[1], new Vector3(0, 0, z * 100), Quaternion.identity);
                oblivionCount++;
                //QuoteManager
                Destroy(buffer0);
            }
        }
        else
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
            else
            {
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
            secret.SetSecretUnlocked(true);
            startUI.GetComponentInChildren<SecretMessage>().SetText("Play! you are in the end game now!");
        }
        else
        {
            startUI.GetComponentInChildren<SecretMessage>().SetText("You need 500 lemons for that");
        }
    }
}
