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
        }
        else
        {
            buttonStartTheGame.gameObject.SetActive(true);
        }
    }

    void SetUsername()
    {
        DataUsername.UserName = playerInputField.text;
        Debug.Log(DataUsername.UserName);
        DataUsername.SaveUsername();
    }
}
