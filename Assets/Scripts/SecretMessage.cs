using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SecretMessage : MonoBehaviour {
    public TextMeshProUGUI text;
    
    public void SetText(string s)
    {
        text.SetText(s);
        Invoke("RemoveText", 3);
    }

    public void RemoveText()
    {
        text.SetText("");
    }
}
