using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuoteManager : MonoBehaviour
{

    [SerializeField]
    public static int[,] quoteSpawn;
    public static bool quoteSet = false;

    public static QuoteManager instance;

    int quoteNumber;
    int counter;

    int oblivionCounter = 0;

    private void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
            oblivionCounter = 0;
            //Debug.Log("instance created");
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    public void SetQuote(GameObject ground, int r)
    {
        int len = ground.GetComponentInChildren<SetText>().getTotal();
        ground.GetComponentInChildren<SetText>().setText(Random.Range(0, len));
    }

    public void SetUpQuoteManager(int groundPrefablength, int offset)
    {
        if (!quoteSet)
        {
            //Debug.Log("quoteSet called");
            quoteSpawn = new int[groundPrefablength, offset];
            int i = 0;
            int j = 0;
            for (i = 0; i < groundPrefablength; i++)
            {
                for (j = 0; j < offset; j++)
                {
                    quoteSpawn[i, j] = 1;
                }
            }
            quoteSet = true;
        }
        else
        {
            //Debug.Log("Quotes Already Set");
        }
    }

    public void SetQuoteWithProbability(GameObject ground, int r)
    {
        // Debug.Log("quoteSetwithprobability called");

        if (Random.Range(0, 9) > 5 || counter >= 10)
        {
            int len = ground.GetComponentInChildren<SetText>().getTotal();
            quoteNumber = Random.Range(0, len);
            for (int i = 0; i < len; i++)
            {
                if (quoteSpawn[r, quoteNumber] == 0)
                {
                    quoteNumber += 1;
                    if (quoteNumber >= len)
                    {
                        quoteNumber = 0;
                    }
                }
                else
                {
                    break;
                }
            }
            //Debug.Log("requesting quote with : " + quoteNumber + " " + r + " and quoteSpawn : " + quoteSpawn[r, quoteNumber]);
            if (quoteSpawn[r, quoteNumber] != 0)
            {
                ground.GetComponentInChildren<SetText>().setText(quoteNumber);
                quoteSpawn[r, quoteNumber] = 0;
            }
            counter = 0;
        }
        else
        {
            counter++;
        }
    }

    public void SetOblivionQuotes(GameObject ground)
    {
        ground.GetComponentInChildren<SetText>().setText(oblivionCounter);
        oblivionCounter++;
    }

}