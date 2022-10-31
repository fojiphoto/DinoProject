using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public AudioSource bgMusicAudioSource, soundfxAudioSource, loopingSoundAudioSource;

    public AudioClip menuMusic, gameplayMusic;

    public SoundClipData[] clipsData;

    private Dictionary<SoundClip, AudioClip> clips;

    public static SoundsManager Instance;

    private void Start()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        UpdateSoundMusicStatus();

        clips = new Dictionary<SoundClip, AudioClip>();
        foreach (var data in clipsData)
        {
            clips.Add(data.clipName, data.clip);
        }
    }

    private void OnEnable()
    {
        GameManager.OnStateChangedEvent += GameManager_OnStateChangedEvent;
    }

    private void OnDisable()
    {
        GameManager.OnStateChangedEvent -= GameManager_OnStateChangedEvent;
    }

    private void GameManager_OnStateChangedEvent(GameState state)
    {
        if (PreferenceManager.isMusicOn)
        {
            if (state == GameState.MAINMENU)
            {
                bgMusicAudioSource.clip = menuMusic;
            }
            else if (state == GameState.GAMEPLAY)
            {
                bgMusicAudioSource.clip = gameplayMusic;
            }
            if (!bgMusicAudioSource.isPlaying)
            {
                bgMusicAudioSource.Play();
            }
        }

        if(state == GameState.LEVEL_COMPLETE)
        {
            PlaySound(SoundClip.LEVEL_COMPLETE);
            bgMusicAudioSource.Stop();
        }
        else if (state == GameState.LEVEL_FAIL)
        {
            PlaySound(SoundClip.LEVEL_FAILED);
            bgMusicAudioSource.Stop();
        }
    }

    public void UpdateSoundMusicStatus()
    {
        bgMusicAudioSource.mute = !PreferenceManager.isMusicOn;
        soundfxAudioSource.mute = !PreferenceManager.isSoundOn;
        loopingSoundAudioSource.mute = !PreferenceManager.isSoundOn;
    }

    public void PlaySound(SoundClip clipName, bool isLoop = false)
    {
        if (PreferenceManager.isSoundOn)
        {
            if (isLoop)
            {
                loopingSoundAudioSource.clip = clips[clipName];
                loopingSoundAudioSource.Play();
            }
            else
            {
                soundfxAudioSource.PlayOneShot(clips[clipName]);
            }
        }
    }

    public void StopLoopingSound()
    {
        loopingSoundAudioSource.Stop();
        loopingSoundAudioSource.clip = null;
    }
}