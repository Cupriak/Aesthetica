using System;
using UnityEngine.Audio;
using UnityEngine;

/// <summary>
/// Class that is responsible for playing most of sounds in game
/// </summary>
public class AudioManager : MonoBehaviour
{
    /// <summary>
    /// Array of Sounds that could be played
    /// </summary>
    [SerializeField] private Sound[] sounds;

    /// <summary>
    /// Call on object creation.
    /// Initialize sounds and start playing level theme
    /// </summary>
    private void Awake()
    {
        SetSounds();
        Play("LevelTheme");
    }

    /// <summary>
    /// Initialization of sounds
    /// </summary>
    private void SetSounds()
    {
        foreach(Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    /// <summary>
    /// Change volume of clip
    /// </summary>
    /// <param name="clipName">Name of clip</param>
    /// <param name="volume">Desired volume (between 0-1)</param>
    public void ChangeVolume(string clipName, float volume)
    {
        Sound sound = Array.Find(sounds, s => s.name == clipName);
        if (sound != null)
        {
            sound.source.volume = Mathf.Clamp01(volume);
        }
        else
        {
            Debug.LogWarning("Sound = " + clipName + " does not exist!");
        }
    }

    /// <summary>
    /// Change pitch of clip
    /// </summary>
    /// <param name="clipName">Name of clip</param>
    /// <param name="pitch">Desired pitch (between 0-1)</param>
    public void ChangePitch(string clipName, float pitch)
    {
        Sound sound = Array.Find(sounds, s => s.name == clipName);
        if (sound != null)
        {
            sound.source.pitch = Mathf.Clamp(pitch, -3f, 3f);
        }
        else
        {
            Debug.LogWarning("Sound = " + clipName + " does not exist!");
        }
    }

    /// <summary>
    /// Play clip
    /// </summary>
    /// <param name="clipName">Name of clip</param>
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
