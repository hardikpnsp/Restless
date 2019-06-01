using UnityEngine;
using TMPro;

public class TotalScore : MonoBehaviour {

    public TextMeshProUGUI text;

    public void SetTotalScore(string s)
    {
        text.SetText(s);
    }
}
