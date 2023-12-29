using System;
using UnityEngine;

[System.Serializable]
public class Sound
{
    [SerializeField] private string _name;

    [SerializeField] private AudioClip _clip;

    [Range(0f, 1f)]
    [SerializeField] private float _volume;
    
    [Range(.1f, 1.0f)]
    [SerializeField] private float _pitch;

    [SerializeField] private bool _isLooped;

    private AudioSource _source;

    #region Getters
    public string GetName()
    {
        return _name;
    }

    public AudioClip GetClip()
    {
        return _clip;
    }

    public float GetVolume()
    {
        return _volume;
    }

    public float GetPitch()
    {
        return _pitch;
    }

    public bool IsLooped()
    {
        return _isLooped;
    }

    public AudioSource GetSource()
    {
        return _source;
    }
    #endregion
    
    #region Setters

    public void SetName(string name)
    {
        _name = name;
    }

    public void SetClip(AudioClip clip)
    {
        _clip = clip;
    }

    public void SetVolume(float volume)
    {
        _volume = volume;
    }

    public void SetPitch(float pitch)
    {
        _pitch = pitch;
    }

    public void SetLoop(bool loop)
    {
        _isLooped = loop;
    }

    public void SetSource(AudioSource source)
    {
        _source = source;
    }
    #endregion
}
