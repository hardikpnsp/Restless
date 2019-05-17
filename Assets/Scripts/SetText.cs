using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetText : MonoBehaviour {


    public string[] quotes;
    public int[] quotesSpawn;
    public TextMeshPro text;
    // Use this for initialization

    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setText(int r)
    {
        if(r >= quotes.Length)
        {
            text.SetText("");
        }
        else
        {
            text.SetText(quotes[r]);
        }
        
    }
}
