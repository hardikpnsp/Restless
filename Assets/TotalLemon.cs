using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TotalLemon : MonoBehaviour {
    public TextMeshProUGUI text;

    public void SetTotalLemon(string s)
    {
        text.SetText(s);
    }
}
