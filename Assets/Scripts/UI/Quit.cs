using Unity.VisualScripting;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    private void Start()
    {
        MMButtonsBeh.OnExitButtonPushed.AddListener(ExitApplication);
    }

    public void ExitApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
