using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScrennsMMController : MonoBehaviour
{
    [SerializeField] GameObject buttons;
    [SerializeField] GameObject loadingScreen;
    [SerializeField] GameObject settingsScreen;
    GameObject[] _allMenus; 

    void Start()
    {
        _allMenus = new GameObject [] { buttons, loadingScreen, settingsScreen};
        MMButtonsBeh.ContButtonPushed.AddListener(ShowLoadingScreen);
        MMButtonsBeh.NGButtonPushed.AddListener(ShowLoadingScreen);
        MMButtonsBeh.SettingsButtonPushed.AddListener(ToggleSettingsScreen);
    }

    void ShowLoadingScreen()
    {
        loadingScreen.gameObject.SetActive(true);
    }

    bool _isSettinsActive;
    public void ToggleSettingsScreen()
    {
        if (!_isSettinsActive)
        {
            settingsScreen.gameObject.SetActive(true);
            HideButtons();
            _isSettinsActive = true;
        }
        else
        {
            settingsScreen.gameObject.SetActive(false);
            ShowButtons();
            _isSettinsActive = false;
        }
    }

    void HideButtons()
    { buttons.gameObject.SetActive(false); }    

    void ShowButtons()
    { 
        for(int i = 0; i < _allMenus.Length; i++)
            _allMenus[i].SetActive(false);

        buttons.gameObject.SetActive(true); 
    }
}
