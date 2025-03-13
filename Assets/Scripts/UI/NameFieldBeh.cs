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
        MMButtonsBeh.OnNameRandomizerButtonPushed.AddListener(GenerateNewName);
    }

    string[] minoanNames = new string[]
    {
    "Минос",
    "Ариадна",
    "Пасифая",
    "Дедал",
    "Икар",
    "Европа",
    "Радамант",
    "Астерион",
    "Талос",
    "Сарпедон",
    "Крес",
    "Мартинос",
    "Евгенос",
    "Иннасеида",
    "Валентинос",
    "Сергиос",
    "Шизикус",
    "Никос",
    "Александр",
    "Идоминеус",
    "Федра",
    "Бритомартис",
    "Ида",
    "Диктис",
    "Кносс"
    };

    private void GenerateNewName()
    {
        int index = Random.Range(0, minoanNames.Length);
        playerInputField.text = minoanNames[index];
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
