using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class HighScore : MonoBehaviour {
    public TextMeshProUGUI text;
    
    public void SetHighScore(string s)
    {
        text.SetText(s);
    }
}
