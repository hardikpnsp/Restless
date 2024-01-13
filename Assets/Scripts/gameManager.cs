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

    public int totalScore;

    public int unlockedLevels = 0;
    private void Start()
    {

        //FindObjectOfType<AdManager>().ShowSkipableAd();

        highScore = PlayerPrefs.GetInt("HighScore", 0);
        totalLemons = PlayerPrefs.GetInt("Lemons", 0);
        totalScore = PlayerPrefs.GetInt("TotalScore", 1);
        unlockedLevels = PlayerPrefs.GetInt("UnlockedLevels", 5);



        /*
        int d = 50;
        
        if(totalScore <= 100)
        {
            d = 50;
        }else if(totalScore < 500)
        {
            d = 100;
        }else if(totalScore < 1000)
        {
            d = 160;
        }
        else if(totalScore < 2500)
        {
            d = 250;
        }
        else
        {
            d = 300;
        }

        int calcUnlockedLevels = (int) totalScore/d + 4;
        if (unlockedLevels != calcUnlockedLevels && unlockedLevels != groundPrefabs.Length)
        {
            if(calcUnlockedLevels > groundPrefabs.Length && unlockedLevels < groundPrefabs.Length && calcUnlockedLevels > unlockedLevels)
            {
                startUI.GetComponentInChildren<SecretMessage>().SetText(
                    "You have unlocked " +
                    (groundPrefabs.Length - unlockedLevels) +
                    " new levels, keep playing!");
                unlockedLevels = groundPrefabs.Length;
                PlayerPrefs.SetInt("UnlockedLevels", unlockedLevels);
            }
            else if(calcUnlockedLevels > unlockedLevels)
            {
                startUI.GetComponentInChildren<SecretMessage>().SetText(
                    "You have unlocked " +
                    (calcUnlockedLevels - unlockedLevels) +
                    " new levels, keep playing!");
                unlockedLevels = calcUnlockedLevels;
                PlayerPrefs.SetInt("UnlockedLevels", unlockedLevels);
            }
        }
        */

        //level unlcok system begins
        /*
        if (totalScore > 2000 && unlockedLevels < 20)
        {
            startUI.GetComponentInChildren<SecretMessage>().SetText(
                   "You have unlocked " +
                   (20 - unlockedLevels) +
                   " new levels, keep playing!");
            unlockedLevels = 20;
            PlayerPrefs.SetInt("UnlockedLevels", unlockedLevels);
        }else if (totalScore > 1000 && unlockedLevels < 15)
        {
            startUI.GetComponentInChildren<SecretMessage>().SetText(
                    "You have unlocked " +
                    (15 - unlockedLevels) +
                    " new levels, keep playing!");
            unlockedLevels = 15;
            PlayerPrefs.SetInt("UnlockedLevels", unlockedLevels);
        }else if(totalScore > 500 && unlockedLevels < 10)
        {
            startUI.GetComponentInChildren<SecretMessage>().SetText(
                   "You have unlocked " +
                   (10 - unlockedLevels) +
                   " new levels, keep playing!");
            unlockedLevels = 10;
            PlayerPrefs.SetInt("UnlockedLevels", unlockedLevels);
        }
        else
        {
            unlockedLevels = 6;
        }

        if(unlockedLevels > groundPrefabs.Length)
        {
            unlockedLevels = groundPrefabs.Length;
        }
        */
        if (totalScore < 100)
        {
            unlockedLevels = 5;
        }
        else if (totalScore < 600)
        {
            unlockedLevels = 10;
        }
        else if(totalScore < 1500)
        {
            unlockedLevels = 15;
        }else if(totalScore < 2500)
        {
            unlockedLevels = 20;
        }
        else if(totalScore < 3500)
        {
            unlockedLevels = 25;
        }else if(totalScore < 5000)
        {
            unlockedLevels = 30;
        }
        {
            unlockedLevels = groundPrefabs.Length;
        }
        if(unlockedLevels > groundPrefabs.Length)
        {
            unlockedLevels = groundPrefabs.Length;
        }
        //unlockedLevels = groundPrefabs.Length;
        //level unlock system end

        if(unlockedLevels != PlayerPrefs.GetInt("unlockedLevels"))
        {
            startUI.GetComponentInChildren<SecretMessage>().SetText(
                   "You have unlocked " +
                   (unlockedLevels - PlayerPrefs.GetInt("unlockedLevels")) +
                   " new levels, keep playing!");
            PlayerPrefs.SetInt("unlockedLevels", unlockedLevels);
        }

        Debug.Log("unlockedLevels : " + unlockedLevels);

        startUI.GetComponentInChildren<HighScore>().SetHighScore("highscore: " + highScore);
        startUI.GetComponentInChildren<TotalLemon>().SetTotalLemon("Lemons: " + totalLemons);
        startUI.GetComponentInChildren<TotalScore>().SetTotalScore("TotalScore: " + totalScore);

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
            //FindObjectOfType<AdManager>().showAd = true;
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
            totalScore = PlayerPrefs.GetInt("TotalScore", 1);
            totalScore += s.getScore();
            PlayerPrefs.SetInt("TotalScore", totalScore);
        }
        else
        {
            deathUI.GetComponentInChildren<score>().setScore(42);
            endGameLemonScore.SetLemons(42);
            totalLemons = PlayerPrefs.GetInt("Lemons", 0);
            PlayerPrefs.SetInt("Lemons", totalLemons + 42);
            totalScore = PlayerPrefs.GetInt("TotalScore", 1);
            totalScore += 42;
            PlayerPrefs.SetInt("TotalScore", totalScore);
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
                int r = Random.Range(0, unlockedLevels);
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
                int r = Random.Range(1, unlockedLevels);
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
                FindObjectOfType<AudioManager>().Play("power-transition", false);
                ps.SetPowerState((int)PowerState.PowerStates.EGO);
                ps.StateChange();
                pc.StateChange();
                movement.StateChange();
                CancelInvoke("goNormal");
                Invoke("goNormal", 10);
            }
            else if (ps.powerState == (int)PowerState.PowerStates.EGO)
            {
                CancelInvoke("goNormal");
                Invoke("goNormal", 10);
            } 
            else if(ps.powerState == (int)PowerState.PowerStates.ANXIETY)
            {
                FindObjectOfType<AudioManager>().Play("power-transition", false);
                CancelInvoke("goNormal");
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
        Debug.Log("going normal");
        if (ps.powerState == (int)PowerState.PowerStates.EGO)
        {
            ps.SetPowerState((int)PowerState.PowerStates.TRANSITIONEGO);
            Debug.Log("ok");
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
            ps.powerState == (int)PowerState.PowerStates.TRANSITIONANX)
            {
                FindObjectOfType<AudioManager>().Play("power-transition", false);
                ps.SetPowerState((int)PowerState.PowerStates.ANXIETY);
                ps.StateChange();
                pc.StateChange();
                movement.StateChange();
                CancelInvoke("goNormal");
                Invoke("goNormal", 20);
            }
            else if(ps.powerState == (int)PowerState.PowerStates.ANXIETY)
            {
                CancelInvoke("goNormal");
                Invoke("goNormal", 20);
            }
            else if(ps.powerState == (int)PowerState.PowerStates.EGO || 
                ps.powerState == (int)PowerState.PowerStates.TRANSITIONEGO)
            {
                FindObjectOfType<AudioManager>().Play("power-transition", false);
                CancelInvoke("goNormal");
                goNormal();
                Invoke("gotAnxiety", 3);
            }
        }
    }


    public void gotAnxiety()
    {
        if (ps.powerState == (int)PowerState.PowerStates.NORMAL ||
            ps.powerState == (int)PowerState.PowerStates.CALM ||
            ps.powerState == (int)PowerState.PowerStates.TRANSITIONANX)
        {
            ps.SetPowerState((int)PowerState.PowerStates.ANXIETY);
            ps.StateChange();
            pc.StateChange();
            movement.StateChange();
            CancelInvoke("goNormal");
            Invoke("goNormal", 20);
        }
        else if (ps.powerState == (int)PowerState.PowerStates.ANXIETY)
        {
            CancelInvoke("goNormal");
            Invoke("goNormal", 20);
        }
        else if (ps.powerState == (int)PowerState.PowerStates.EGO ||
            ps.powerState == (int)PowerState.PowerStates.TRANSITIONEGO)
        {
            CancelInvoke("goNormal");
            goNormal();
            Invoke("gotAnxiety", 1);
            Debug.Log("okok");
            /*
            ps.SetPowerState((int)PowerState.PowerStates.ANXIETY);
            ps.StateChange();
            pc.StateChange();
            movement.StateChange();
            Invoke("goNormal", 20);
            */
        }
    }

    public void OpenSecret()
    {
        totalLemons = PlayerPrefs.GetInt("Lemons", 0);
        if (totalLemons >= 100)
        {
            secret.SetSecretUnlocked(true);
            startUI.GetComponentInChildren<SecretMessage>().SetText("Play! you are in the end game now!");
        }
        else
        {
            startUI.GetComponentInChildren<SecretMessage>().SetText("You need 100 lemons for that");
        }
    }
}
