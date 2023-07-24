using System;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public Sound[] sounds;
    public AudioMixerGroup mixerGroup;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            sound.source.outputAudioMixerGroup = mixerGroup;
        }
    }

    public void PlaySound(string name)
{
    Sound sound = Array.Find(sounds, s => s.name == name);
    if (sound == null)
    {
        Debug.LogWarning("Sound: " + name + " not found!");
        return;
    }

    sound.source.Play();
}


}
