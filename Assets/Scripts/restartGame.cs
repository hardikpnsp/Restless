using UnityEngine;
using UnityEngine.SceneManagement;

public class restartGame : MonoBehaviour {
    
    void loadGame()
    {
        FindObjectOfType<gameManager>().Restart();
    }
}
