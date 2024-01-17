using UnityEngine;

public class SecretUnlocked : MonoBehaviour {

    public static bool secretUnlocked = false;

    public static SecretUnlocked instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    public void SetSecretUnlocked(bool b)
    {
        secretUnlocked = b;
    }

    public bool GetSecret()
    {
        return secretUnlocked;
    }
}
