using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public Sound[] sounds;

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
            sound.source.playOnAwake = sound.playOnAwake;
        }
    }

    // Add methods for playing, stopping, and adjusting volume/pitch of sounds.


    public void PlaySound(string name)
{
    Sound sound = System.Array.Find(sounds, s => s.name == name);
    if (sound == null)
    {
        Debug.LogWarning("Sound: " + name + " not found!");
        return;
    }
    Debug.Log($"Played {name}");

    sound.source.Play();
}


}
