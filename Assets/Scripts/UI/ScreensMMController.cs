using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScreensMMController : MonoBehaviour
{
    [SerializeField] GameObject buttons;
    [SerializeField] GameObject loadingScreen;
    [SerializeField] GameObject chooseWhatToLoadScreen;
    [SerializeField] GameObject settingsScreen;
    [SerializeField] GameObject achievmentsScreen;
    [SerializeField] GameObject creditsScreen;

    private GameObject _currentActiveScreen;
    private List<GameObject> _allMenus;

    void Start()
    {
        _allMenus = new List<GameObject>
        {
            loadingScreen,
            chooseWhatToLoadScreen,
            settingsScreen,
            achievmentsScreen,
            creditsScreen
        };

        MMButtonsBeh.ContButtonPushed.AddListener(() => ShowScreen(loadingScreen));
        MMButtonsBeh.NGButtonPushed.AddListener(() => ShowScreenWithDelay(loadingScreen, 1f));
        MMButtonsBeh.LoadsButtonPushed.AddListener(() => ToggleScreen(chooseWhatToLoadScreen));
        MMButtonsBeh.SettingsButtonPushed.AddListener(() => ToggleScreen(settingsScreen));
        MMButtonsBeh.AchievmentsButtonPushed.AddListener(() => ToggleScreen(achievmentsScreen));
        MMButtonsBeh.CreditButtonPushed.AddListener(() => ToggleScreen(creditsScreen));
    }

    async void ShowScreenWithDelay(GameObject screen, float delayInSeconds)
    {
        await UniTask.Delay((int)(delayInSeconds * 1000));
        ShowScreen(screen);
    }


    void ShowScreen(GameObject screen)
    {
        HideAllScreens();
        screen.SetActive(true);
        _currentActiveScreen = screen;
    }

    void ToggleScreen(GameObject screen)
    {
        if (_currentActiveScreen == screen)
        {
            screen.SetActive(false);
            _currentActiveScreen = null;
            ShowButtons();
        }
        else
        {
            ShowScreen(screen);
            HideButtons();
        }
    }

    void HideAllScreens()
    {
        foreach (var menu in _allMenus)
        {
            menu.SetActive(false);
        }
    }

    void HideButtons()
    {
        buttons.SetActive(false);
    }

    void ShowButtons()
    {
        HideAllScreens();
        buttons.SetActive(true);
    }
}
