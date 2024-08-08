using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Класс для управления UI элементами взаимодействующими со звуком
/// </summary>
public class UIAudioController : MonoBehaviour
{
    public Slider musicSlider, sfxSlider, masterSlider, voiceTrackSlider;

    public Toggle audioToggle;

    private void Start()
    {
        InitializeSliders();
    }

    /// <summary>
    /// Включает/выключает звук
    /// </summary>
    public void AudioToggle()
    {
        AudioManager.Instance.AudioToggle(audioToggle.isOn);

        ///Включает/выключает активность слайдеров
        musicSlider.interactable = !audioToggle.isOn;
        sfxSlider.interactable = !audioToggle.isOn;
        masterSlider.interactable = !audioToggle.isOn;
        voiceTrackSlider.interactable= !audioToggle.isOn;
    }

    /// <summary>
    /// Устанавливает громкость фонофой музыки
    /// </summary>
    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(musicSlider.value);
    }

    /// <summary>
    /// Устанавливает громкость звуковых эффектов
    /// </summary>
    public void SFXVolume()
    {
        AudioManager.Instance.SFXVolume(sfxSlider.value);
    }

    /// <summary>
    /// Устанавливает общую громкость
    /// </summary>
    public void MasterVolume()
    {
        AudioManager.Instance.MasterVolume(masterSlider.value);
    }

    /// <summary>
    /// Устанавливает громкость звуковых дорожек
    /// </summary>
    public void VoiceTrackVolume()
    {
       AudioManager.Instance.VoiceTrackVolume(voiceTrackSlider.value);
    }

    /// <summary>
    /// Устанавливает положение слайдеров при загрузке
    /// </summary>
    private void InitializeSliders()
    {
        if (AudioManager.Instance != null)
        {
            if (musicSlider != null)
                musicSlider.value = PlayerPrefs.GetFloat(AudioManager.MUSIC_VOLUME,1f);

            if (sfxSlider != null)
                sfxSlider.value = PlayerPrefs.GetFloat(AudioManager.SFX_VOLUME,1f);

            if (masterSlider != null)
                masterSlider.value = PlayerPrefs.GetFloat(AudioManager.MASTER_VOLUME, 1f);

            if (voiceTrackSlider != null)
                voiceTrackSlider.value = PlayerPrefs.GetFloat(AudioManager.VOICE_TRACK_VOLUME,1f);

            if (audioToggle != null)
            {
                audioToggle.isOn = PlayerPrefs.GetInt(AudioManager.MUTE) == 1;
            }
        }
    }
}
