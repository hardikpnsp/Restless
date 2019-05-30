
using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]

public class Sound {

    public int id;
    public int stream; 

    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;

    [Range(0.1f, 3)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;

}
