using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void OnEnable()
    {
        GameManager.onResetGame.AddListener(ResetMusicSource);
        GameManager.onWinGame.AddListener(AudioOnWinGame);
    }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic("GameplayMusic");
    }

    public void PlayMusic(string musicName)
    {
        var s = Array.Find(musicSounds, x => x.name == musicName);

        if(s == null)
        {
            return;
        }
        musicSource.clip = s.clip;
        musicSource.Play();
    }

    public void StopMusic()
    {
        if (musicSource.clip)
        {
            musicSource.Stop();
        }                   
    }

    public void PlaySfx(string soundName)
    {
        var s = Array.Find(sfxSounds, x => x.name == soundName);

        if(s == null)
        {
            return;
        }
        
        sfxSource.PlayOneShot(s.clip);
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSfx()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SfxVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    private void AudioOnWinGame()
    {
        StopMusic();
        PlaySfx("KingDie");
    }

    private void ResetMusicSource()
    {
        musicSource.Stop();
        musicSource.Play();
    }

    private void OnDisable()
    {
        GameManager.onResetGame.RemoveListener(ResetMusicSource);
        GameManager.onWinGame.RemoveListener(AudioOnWinGame);
    }
}
