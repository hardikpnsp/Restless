using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetText : MonoBehaviour {


    public string[] quotes;
    public TextMeshPro text; 
	// Use this for initialization
	void Start () {
        int r = Random.Range(0, quotes.Length);
        text.SetText(quotes[r]);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
