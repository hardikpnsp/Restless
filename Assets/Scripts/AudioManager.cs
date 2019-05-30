using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour {

    public Sound[] sounds;

    public static AudioManager instance; 

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        //ana
        

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            //Assets/Sounds/breathing.mp3
            //ana

        }
        Sound music = Array.Find(sounds, sound => sound.name == "Neostead_nature");
        music.source.Play();
    }


    public void Start()
    {
        AndroidNativeAudio.makePool();

        foreach(Sound s in sounds)
        {
            s.id = AndroidNativeAudio.load(s.name + ".mp3");
            Debug.Log("file id = " + s.id);
        }
    }
    public void Play(string name, bool loop)
    {
        /*
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found!");
            return;
        }
        s.source.Play();
        */
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found!");
            return;
        }
        //s.id = AndroidNativeAudio.load(s.name + ".mp3");
        if (s.id != 0 && s.stream != 0)
        {
            s.stream = AndroidNativeAudio.play(s.id);
            Debug.Log("the stream : " + s.stream);
        }
        else
        {
            AndroidNativeAudio.resume(s.stream);
        }
        if (loop)
        {
            AndroidNativeAudio.setLoop(s.stream, -1);
        }
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound: " + name + "Can't be stopped");
            return;
        }
        AndroidNativeAudio.unload(s.id);
    }

    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "Can't be stopped");
            return;
        }
        AndroidNativeAudio.pause(s.stream);
    }

    private void OnDestroy()
    {
        AndroidNativeAudio.releasePool();
    }
}
