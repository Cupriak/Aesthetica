using UnityEngine.Audio;
using UnityEngine;

/// <summary>
/// Class that consist sound attributes
/// </summary>
[System.Serializable]
public class Sound
{
    /// <summary>
    /// Name of sound
    /// </summary>
    public string name;
    /// <summary>
    /// Audio clip of sound
    /// </summary>
    public AudioClip clip;
    /// <summary>
    /// Volume of sound in range between 0 and 1
    /// </summary>
    [Range(0f, 1f)] public float volume;
    /// <summary>
    /// Pitch of sound in range between -3 and 3
    /// </summary>
    [Range(-3f, 3f)] public float pitch;
    /// <summary>
    /// Flag that determines if sound should be looped
    /// </summary>
    public bool loop;
    /// <summary>
    /// Audio source of the sound
    /// </summary>
    [HideInInspector] public AudioSource source;
}
