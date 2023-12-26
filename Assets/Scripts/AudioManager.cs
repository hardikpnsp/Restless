using UnityEngine.Audio;
using UnityEngine;
using System;

public interface IAudioManager {
    public void InitialLoad(Sound[] sounds, Action _loadCompleteCallbacks);
    public void Pause(Sound sound);
    public void Play(Sound sound, bool loop);
    public void Stop(Sound sound);
    public void Resume(Sound sound, bool loop);
}

public class AndroidAudioManager : IAudioManager
{
    private int soundLoadedCount = 0;
    public void Pause(Sound s)
    {
        if (s.isMusic) {
            ANAMusic.pause(s.id);
        } else {
            AndroidNativeAudio.pause(s.stream);
        }
    }

    public void Play(Sound s, bool loop)
    {
        if (s.isMusic) {
            if (!ANAMusic.isPlaying(s.id))
            {
                ANAMusic.play(s.id);
                ANAMusic.setLooping(s.id, loop);
                ANAMusic.setVolume(s.id, s.volume);
            }
        } else {
            if (loop)
            {
                s.stream = AndroidNativeAudio.play(s.id, leftVolume: s.volume, loop: -1);
            }
            else
            {
                s.stream = AndroidNativeAudio.play(s.id, leftVolume: s.volume);
            }
            s.streamSet = true;    
        }
    }

    public void Resume(Sound s, bool loop)
    {
        if (s.isMusic) {
            if (!ANAMusic.isPlaying(s.id))
            {
                ANAMusic.play(s.id);
                ANAMusic.setLooping(s.id, loop);
            }
        } else {
            if (s.streamSet)
            {
                AndroidNativeAudio.resume(s.stream);
            }
            else
            {
                if (loop)
                {
                    s.stream = AndroidNativeAudio.play(s.id, leftVolume: s.volume, loop: -1);
                }
                else
                {
                    s.stream = AndroidNativeAudio.play(s.id, leftVolume: s.volume);
                }
                s.streamSet = true;
            }
        }
    }

    public void InitialLoad(Sound[] sounds, Action _loadCompleteCallback)
    {
        AndroidNativeAudio.makePool();

        foreach(Sound s in sounds)
        {
            if (s.isMusic) {
                ANAMusic.load(s.name + ".mp3", loadedCallback: SoundLoadedCallback(s, sounds, _loadCompleteCallback));
            } else {
                AndroidNativeAudio.load(s.name + ".mp3", callback: SoundLoadedCallback(s, sounds, _loadCompleteCallback));
            }
        }

        Action<int> SoundLoadedCallback(Sound s, Sound[] sounds, Action _loadCompleteCallback)
        {
            return (i) =>
            {
                Debug.Log("id for " + s.name + " = " + s.id);
                soundLoadedCount++;
                s.id = i;
                Debug.Log("Sound loading complete:" + soundLoadedCount);

                if (soundLoadedCount == sounds.Length)
                {
                    Debug.Log("All sounds loaded");
                    _loadCompleteCallback();
                }
            };
        }
    }

    public void Stop(Sound s)
    {
        if (s.isMusic) {
            ANAMusic.release(s.id);
        } else {
            AndroidNativeAudio.unload(s.id);
        }
    }
}

public class DefaultAudioManager : IAudioManager
{
    public void Pause(Sound s)
    {
        s.source.Pause();
    }

    public void Play(Sound s, bool loop)
    {
        if (loop)
        {
            s.source.loop = true;
            s.source.Play();
        }
        else
        {
            s.source.loop = false;
            s.source.Play();
        }
        s.streamSet = true;  
    }

    public void Resume(Sound s, bool loop)
    {
        if (s.streamSet)
        {
            s.source.UnPause();
        }
        else
        {
            if (loop)
            {
                s.source.loop = true;
                s.source.Play();
            }
            else
            {
                s.source.loop = false;
                s.source.Play();
            }
        }
    }

    public void InitialLoad(Sound[] sounds, Action _loadCompleteCallback)
    {
        _loadCompleteCallback();
    }

    public void Stop(Sound s)
    {
        s.source.Stop();
    }
}

public class AudioManager : MonoBehaviour {

    public Sound[] sounds;

    public static AudioManager instance;

    public static IAudioManager am;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            #if UNITY_ANDROID && !UNITY_EDITOR
            am = new AndroidAudioManager();
            #else
            am = new DefaultAudioManager();
            #endif
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.streamSet = false;
        }
    }


    public void Start()
    {
        am.InitialLoad(sounds, () => {
            instance.Play("Neostead_nature", loop: true);
            instance.Play("Outdoor_Ambiance", loop: true);
            instance.Play("breathing", loop: true);
        });
    }
    public void Play(string name, bool loop)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found!");
            return;
        }

        am.Play(s, loop);
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound: " + name + "Can't be stopped");
            return;
        }
        am.Stop(s);
    }

    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "Can't be stopped");
            return;
        }
        am.Pause(s);
    }


    public void Resume(string name, bool loop)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found!");
            return;
        }
        am.Resume(s, loop);
    }
}