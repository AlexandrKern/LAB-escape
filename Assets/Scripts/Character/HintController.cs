using TMPro;
using UnityEngine;
using DG.Tweening;

public class HintController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hintText;
    [SerializeField] string takeTheFormOfSwarmHint = "Примите форму роя для взаимодейстия";
    [SerializeField] string pressQForVent = "Нажмите Q чтобы пробраться по вентиляции";
    [SerializeField] string pressQForObstacle = "Нажмите Q для преодоления препятствия";
    [SerializeField] string hummerFormEnabled = "Открыта форма молота";
    [SerializeField] string hummerHintPress2 = "Нажмите 2 для превращения в молот\nНажмите 1 для превращения в рой\nНажмите E, приняв форму роя,\nдля взаимодействия с терминалом";
    [SerializeField] string hintCanBeCrushed = "Стеклянные и другие непрочные объекты\nможно разбить молотом";
    [SerializeField] string columnCanBeCrushed = "Колонна не выглядит очень прочной";
    [SerializeField] string realGame = "The real lab escape starts here";

    void Start()
    {
        string[] connectedJoysticks = Input.GetJoystickNames();

        if (connectedJoysticks.Length > 0)
        {
            foreach (string joystickName in connectedJoysticks)
            {
                if (!string.IsNullOrEmpty(joystickName))
                {
                    pressQForVent = "Нажмите X чтобы пробраться по вентиляции";
                    pressQForObstacle = "Нажмите X для преодоления препятствия";
                    hummerHintPress2 = "Нажмите RB для превращения в молот\nНажмите LB для превращения в рой\nНажмите Y, приняв форму роя,\nдля взаимодействия с терминалом";
                }
            }
        }
        else
        {
            Debug.Log("Геймпад не подключен.");
        }
    }

    public void SetHintTextRotation(float value)
    {
        Vector3 hintTextRotation = hintText.transform.rotation.eulerAngles;
        hintTextRotation.y = value;
        hintText.transform.rotation = Quaternion.Euler(hintTextRotation);
    }

    public void CleanTheHint()
    {
        hintText.DOFade(0, 0.5f);
    }

    private void ShowHint(string hint, float durationOfShow)
    {
        hintText.DOFade(0, 0);
        hintText.text = hint;
        hintText.DOFade(1, 0.5f).OnComplete(() =>
        {
            DOVirtual.DelayedCall(durationOfShow, CleanTheHint);
        });
    }

    public void HintTakeTheFormOfSwarm()
    {
        ShowHint(takeTheFormOfSwarmHint, 3);
    }

    public void HintPressQForObstacle()
    {
        ShowHint(pressQForObstacle, 3);
    }

    public void HintPressQForVentilation()
    {
        ShowHint(pressQForVent, 3);
    }

    public void HintHammerFormEnabled()
    {
        ShowHint(hummerFormEnabled, 3);
    }
    public void HintHammerPress2()
    {
        ShowHint(hummerHintPress2, 60);
    }
    public void HintCanBeCrushed()
    {
        ShowHint(hintCanBeCrushed, 5);
    }

    public void ColumnCanBeCrushed()
    {
        ShowHint(columnCanBeCrushed, 3);
    }

    public void RealGameStartHere()
    {
        ShowHint(realGame, 3);
    }

    private void OnDestroy()
    {
        DOTween.KillAll();
    }
}