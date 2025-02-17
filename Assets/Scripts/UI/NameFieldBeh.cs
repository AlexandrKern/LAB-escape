using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NameFieldBeh : MonoBehaviour
{
    [SerializeField] TMP_InputField playerInputField;
    [SerializeField] Button buttonStartTheGame;

    private void Start()
    {
        MMButtonsBeh.OnNGButtonPushed.AddListener(SetUsername);
    }

    public void NewChar()
    {
        if (string.IsNullOrEmpty(playerInputField.text))
        {
            buttonStartTheGame.gameObject.SetActive(false);
            Debug.Log("строка пустая");
        }
        else
        {
            buttonStartTheGame.gameObject.SetActive(true);
            Debug.Log("строка не пустая");
        }
    }

    void SetUsername()
    {
        DataUsername.UserName = playerInputField.text;
    }
}
