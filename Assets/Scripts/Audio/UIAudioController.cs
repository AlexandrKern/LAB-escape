using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ����� ��� ���������� UI ���������� ������������������ �� ������
/// </summary>
public class UIAudioController : MonoBehaviour
{
    [SerializeField]private Slider _musicSlider, _sfxSlider, _masterSlider, _voiceTrackSlider;

    [SerializeField] private Toggle _audioToggle;

    private void OnEnable()
    {
        InitializeSliders();
    }

    /// <summary>
    /// ��������/��������� ����
    /// </summary>
    public void AudioToggle()
    {
        AudioManager.Instance.AudioToggle(_audioToggle.isOn);

        ///��������/��������� ���������� ���������
        _musicSlider.interactable = !_audioToggle.isOn;
        _sfxSlider.interactable = !_audioToggle.isOn;
        _masterSlider.interactable = !_audioToggle.isOn;
        _voiceTrackSlider.interactable= !_audioToggle.isOn;
    }

    /// <summary>
    /// ������������� ��������� ������� ������
    /// </summary>
    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(_musicSlider.value);
    }

    /// <summary>
    /// ������������� ��������� �������� ��������
    /// </summary>
    public void SFXVolume()
    {
        AudioManager.Instance.SFXVolume(_sfxSlider.value);
    }

    /// <summary>
    /// ������������� ����� ���������
    /// </summary>
    public void MasterVolume()
    {
        AudioManager.Instance.MasterVolume(_masterSlider.value);
    }

    /// <summary>
    /// ������������� ��������� �������� �������
    /// </summary>
    public void VoiceTrackVolume()
    {
       AudioManager.Instance.VoiceTrackVolume(_voiceTrackSlider.value);
    }

    public void SaveAudioSettings()
    {
        AudioManager.Instance.SaveAllVolumes();
    }

    /// <summary>
    /// ������������� ��������� ��������� ��� ��������
    /// </summary>
    private void InitializeSliders()
    {
        if (AudioManager.Instance != null)
        {
            if (_musicSlider != null)
                _musicSlider.value = PlayerPrefs.GetFloat(AudioManager.MUSIC_VOLUME,1f);

            if (_sfxSlider != null)
                _sfxSlider.value = PlayerPrefs.GetFloat(AudioManager.SFX_VOLUME,1f);

            if (_masterSlider != null)
                _masterSlider.value = PlayerPrefs.GetFloat(AudioManager.MASTER_VOLUME, 1f);

            if (_voiceTrackSlider != null)
                _voiceTrackSlider.value = PlayerPrefs.GetFloat(AudioManager.VOICE_TRACK_VOLUME,1f);

            if (_audioToggle != null)
            {
                _audioToggle.isOn = PlayerPrefs.GetInt(AudioManager.MUTE) == 1;
            }
        }
    }

    public void AudioToDefault()
    {
        AudioManager.Instance.AudioToDefault();
        InitializeSliders();
    }
}
