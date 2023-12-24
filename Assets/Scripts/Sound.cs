
using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]

public class Sound {

    [System.NonSerialized]
    public int id;
    [System.NonSerialized]
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

    public bool streamSet = false;
}
