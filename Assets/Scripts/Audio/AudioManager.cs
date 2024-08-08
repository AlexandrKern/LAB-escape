using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Класс для управления изменениями и настройкой звука
/// </summary>
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public Sound[] musicSounds, sfxSounds, voiceTrackSouns;
    public AudioSource musicSource, sfxSource, voiceTrackSource;

    private float _masterVolume = 1f; // Общий уровень громкости
    private float _fadeDuration = 5; // Длительность плавности воспроизведения

    private float _currentMusicVolume;
    private float _musicVolume;
    private float _sfxVolume;
    private float _voiceVolume; 

    public const string MUSIC_VOLUME = "MusicVolume"; 
    public const string SFX_VOLUME = "SFXVolume"; 
    public const string VOICE_TRACK_VOLUME = "VoiceTrackVolume"; 
    public const string MASTER_VOLUME = "MasterVolume";
    public const string MUTE = "Mute"; 

    private void Awake()
    {
        if (Instance == null)
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
        LoadAllVolumes(); 
        PlayMusic("BackGroundMusic");
    }

    private void OnApplicationQuit()
    {
        SaveAllVolumes();
    }

    /// <summary>
    /// Воспроизводит фоновую музыку
    /// </summary>
    /// <param name="name">Назавние трека</param>
    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not found"); 
        }
        else
        {
            musicSource.clip = s.clip; 
            musicSource.Play();
        }
    }

    /// <summary>
    /// Воспроизводит звуковой эффект
    /// </summary>
    /// <param name="name">Название эффекта</param>
    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("SFX not found"); 
        }
        else
        {
            sfxSource.PlayOneShot(s.clip); 
        }
    }

    /// <summary>
    /// Воспроизводит голосовую дорожку
    /// </summary>
    /// <param name="name">Название дорожки</param>
    public void PlayVoiceTrack(string name)
    {
        Sound s = Array.Find(voiceTrackSouns, x => x.name == name); 

        if (s == null)
        {
            Debug.Log("VoiceTrack not found");
        }
        else
        {
            voiceTrackSource.PlayOneShot(s.clip);
        }
    }

    /// <summary>
    /// Включает/выключает звук
    /// </summary>
    /// <param name="mute">Состояние звука</param>
    public void AudioToggle(bool mute)
    {
        musicSource.mute = mute;
        sfxSource.mute = mute; 
        voiceTrackSource.mute = mute; 

        if (!mute)
        {
            StartCoroutine(FadeIn()); 
        }
        else
        {
            StopAllCoroutines();
        }

        PlayerPrefs.SetInt(MUTE, mute ? 1 : 0); 
    }

    /// <summary>
    /// Устанавливает громкость фонофой музыки
    /// </summary>
    /// <param name="volume">Значение громкости</param>
    public void MusicVolume(float volume)
    {
        _currentMusicVolume = volume; 
        musicSource.volume = volume * _masterVolume; 
        _musicVolume = volume; 
    }

    /// <summary>
    /// Устанавливает громкость звуковых эффектов
    /// </summary>
    /// <param name="volume">Значение громкости</param>
    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume * _masterVolume; 
        _sfxVolume = volume; 
    }

    /// <summary>
    /// Устанавливает громкость звуковых дорожек
    /// </summary>
    /// <param name="volume">Значение громкости</param>
    public void VoiceTrackVolume(float volume)
    {
        voiceTrackSource.volume = volume * _masterVolume; 
        _voiceVolume = volume; 
    }

    /// <summary>
    /// Устанавливает общую громкость
    /// </summary>
    /// <param name="volume">Значение громкости</param>
    public void MasterVolume(float volume)
    {
        _masterVolume = volume; 
        ApplyMasterVolume(); 
    }

    /// <summary>
    /// Применяет общую громкость ко всем источникам звука
    /// </summary>
    private void ApplyMasterVolume()
    {
        musicSource.volume = _musicVolume * _masterVolume; 
        sfxSource.volume = _sfxVolume * _masterVolume; 
        voiceTrackSource.volume = _voiceVolume * _masterVolume; 
    }

    /// <summary>
    /// Корутина для плавного увеличения громкости
    /// </summary>
    /// <returns></returns>
    private IEnumerator FadeIn()
    {
        float currentTime = 0f; 
        musicSource.volume = 0f; 

        while (currentTime < _fadeDuration) 
        {
            currentTime += Time.deltaTime; 
            musicSource.volume = Mathf.Lerp(0f, _currentMusicVolume, currentTime / _fadeDuration) * _masterVolume; // Плавно увеличивае громкость
            yield return null; // Ждет до следующего кадра
        }

        musicSource.volume = _currentMusicVolume * _masterVolume; 
    }

    /// <summary>
    /// Сохраняет громкость всех источников
    /// </summary>
    public void SaveAllVolumes()
    {
        PlayerPrefs.SetFloat(MUSIC_VOLUME, _musicVolume); 
        PlayerPrefs.SetFloat(SFX_VOLUME, _sfxVolume);
        PlayerPrefs.SetFloat(VOICE_TRACK_VOLUME, _voiceVolume); 
        PlayerPrefs.SetFloat(MASTER_VOLUME, _masterVolume); 
        PlayerPrefs.Save(); 
    }

    /// <summary>
    /// Загружает громкость всех источников
    /// </summary>
    private void LoadAllVolumes()
    {
        _musicVolume = PlayerPrefs.GetFloat(MUSIC_VOLUME, 1f); 
        _sfxVolume = PlayerPrefs.GetFloat(SFX_VOLUME, 1f); 
        _voiceVolume = PlayerPrefs.GetFloat(VOICE_TRACK_VOLUME, 1f); 
        _masterVolume = PlayerPrefs.GetFloat(MASTER_VOLUME, 1f); 

        _currentMusicVolume = _musicVolume; 

        ApplyMasterVolume(); 
    }

    /// <summary>
    /// Для теста звуков и голоса
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlaySFX("SFXSound_1");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlaySFX("SFXSound_2");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlaySFX("SFXSound_3");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            PlayVoiceTrack("Voice_track");
        }
    }
}
