using UnityEngine.UI;
using UnityEngine;


public class LemonScore : MonoBehaviour {
    public Text score;

    private int totalLemons = 0;
    public void IncreaseLemons()
    {
        totalLemons += 1;
        score.text = totalLemons.ToString();
    }

    public int GetLemons()
    {
        return totalLemons;
    }

    public void SetLemons(int lemons)
    {
        totalLemons = lemons-1;
        IncreaseLemons();
    }

}
