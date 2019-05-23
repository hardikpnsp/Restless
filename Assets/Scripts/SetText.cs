using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetText : MonoBehaviour {

    public string[] quotes;
    public int[] quotesSpawn;
    public TextMeshPro text;

    public void Awake()
    {
        text.SetText("");
    }

    public int getTotal()
    {
        return quotes.Length;
    }

    public void setText(int r)
    {
        if(r >= quotes.Length)
        {
            text.SetText("");
            //Debug.LogWarning("I was requested out of bounds");
        }
        else
        {
            text.SetText(quotes[r]);
        }
        
    }

}
