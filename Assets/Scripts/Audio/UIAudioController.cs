using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ����� ��� ���������� UI ���������� ������������������ �� ������
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
    /// ��������/��������� ����
    /// </summary>
    public void AudioToggle()
    {
        AudioManager.Instance.AudioToggle(audioToggle.isOn);

        ///��������/��������� ���������� ���������
        musicSlider.interactable = !audioToggle.isOn;
        sfxSlider.interactable = !audioToggle.isOn;
        masterSlider.interactable = !audioToggle.isOn;
        voiceTrackSlider.interactable= !audioToggle.isOn;
    }

    /// <summary>
    /// ������������� ��������� ������� ������
    /// </summary>
    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(musicSlider.value);
    }

    /// <summary>
    /// ������������� ��������� �������� ��������
    /// </summary>
    public void SFXVolume()
    {
        AudioManager.Instance.SFXVolume(sfxSlider.value);
    }

    /// <summary>
    /// ������������� ����� ���������
    /// </summary>
    public void MasterVolume()
    {
        AudioManager.Instance.MasterVolume(masterSlider.value);
    }

    /// <summary>
    /// ������������� ��������� �������� �������
    /// </summary>
    public void VoiceTrackVolume()
    {
       AudioManager.Instance.VoiceTrackVolume(voiceTrackSlider.value);
    }

    /// <summary>
    /// ������������� ��������� ��������� ��� ��������
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
