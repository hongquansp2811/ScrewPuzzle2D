using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundInfo
{
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume = 1f;
    public bool loop = false;
    [HideInInspector]
    public AudioSource source;
}

public class SoundManager : Singleton<SoundManager>
{
    public SoundInfo bgMusic;
    public SoundInfo selectBolt;
    public SoundInfo hammer;
    public SoundInfo removeScrew;
    public SoundInfo win;
    public SoundInfo lose;

    private bool isMuted = false;
    private float masterVolume = 1f;

    private void Start()
    {
        InitializeSound(bgMusic);
        InitializeSound(selectBolt);
        InitializeSound(hammer);
        InitializeSound(removeScrew);
        InitializeSound(win);

        PlaySound(bgMusic);
    }

    private void InitializeSound(SoundInfo sound)
    {
        sound.source = gameObject.AddComponent<AudioSource>();
        sound.source.clip = sound.clip;
        sound.source.volume = sound.volume * masterVolume;
        sound.source.loop = sound.loop;
    }

    public void PlaySound(SoundInfo sound)
    {
        if (!isMuted && sound.source != null)
        {
            sound.source.Play();
        }
    }

    public void StopSound(SoundInfo sound)
    {
        if (sound.source != null)
        {
            sound.source.Stop();
        }
    }

    public void SetVolume(float volume)
    {
        masterVolume = volume;

        bgMusic.source.volume = bgMusic.volume * masterVolume;
        selectBolt.source.volume = selectBolt.volume * masterVolume;
        hammer.source.volume = hammer.volume * masterVolume;
        removeScrew.source.volume = removeScrew.volume * masterVolume;
        win.source.volume = win.volume * masterVolume;
    }

    public void MuteSound(bool mute)
    {
        isMuted = mute;

        bgMusic.source.mute = isMuted;
        selectBolt.source.mute = isMuted;
        hammer.source.mute = isMuted;
        removeScrew.source.mute = isMuted;
        win.source.mute = isMuted;
    }
}
