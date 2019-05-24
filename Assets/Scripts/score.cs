using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour {

    public Text scoreText; 
    public Transform player;
    public int playerScore;
    public int bonusScore;
    public int totalScore;
    public bool setScoreCalled = false;
	// Update is called once per frame
	void Update () {
        if (!setScoreCalled)
        {
            playerScore = (int)player.position.z / 10;
            if (player.position.z >= 0)
            {
                if (totalScore < playerScore + bonusScore)
                {
                    totalScore = playerScore + bonusScore;
                }
                scoreText.text = (totalScore).ToString();
            }
        }
	}

    public void addBonusScore(int score)
    {
        bonusScore += score;
    }

    public int getScore()
    {
        return bonusScore + playerScore;
    }

    public void setScore(int s)
    {
        totalScore = s;
        setScoreCalled = true;
        scoreText.text = (totalScore).ToString();
    }
}
