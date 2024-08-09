using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Тестовый класс для проверки работы AudioManager
/// </summary>
public class TestScriptButtonClick : MonoBehaviour
{
    public void PlaySFXOne()
    {
        AudioManager.Instance.PlaySFX("SFXSound_1");
    }
    public void PlaySFXTwo()
    {
        AudioManager.Instance.PlaySFX("SFXSound_2");
    }
    public void PlaySFXThree()
    {
        AudioManager.Instance.PlaySFX("SFXSound_3");
    }
    public void PlayVoiceTrack()
    {
        AudioManager.Instance.PlayVoiceTrack("Voice_track");
    }
}
