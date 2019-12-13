using System;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] sounds;

    private void Awake()
    {
        SetSounds();
        Play("LevelTheme");
    }

    private void SetSounds()
    {
        foreach(Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.loop = sound.loop;
        }
    }

    public void Play(string clipName)
    {
        Sound sound = Array.Find(sounds, s => s.name == clipName);
        if (sound != null)
        {
            sound.source.Play();
        }
        else
        {
            Debug.LogWarning("Sound = " + clipName + " does not exist!");
        }
    }
}
