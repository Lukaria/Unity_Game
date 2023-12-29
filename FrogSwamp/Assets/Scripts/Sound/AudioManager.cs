using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] _sounds;
    void Awake()
    {
        foreach (var sound in _sounds)
        {
            sound.SetSource(gameObject.AddComponent<AudioSource>());
            sound.GetSource().clip = sound.GetClip();
            sound.GetSource().volume = sound.GetVolume();
            sound.GetSource().pitch = sound.GetPitch();
            sound.GetSource().loop = sound.IsLooped();
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(_sounds, sound => sound.GetName() == name);

        if (s == null)
        {
            Debug.Log("No sound named " + name);
            return;
        }
        
        s.GetSource().Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(_sounds, sound => sound.GetName() == name);

        if (s == null)
        {
            Debug.Log("No sound named " + name);
            return;
        }

        if (s.GetSource().isPlaying)
        {
            s.GetSource().Stop();
        }
    }
}
