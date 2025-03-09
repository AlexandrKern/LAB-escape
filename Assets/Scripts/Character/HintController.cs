using TMPro;
using UnityEngine;
using DG.Tweening;

public class HintController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hintText;
    [SerializeField] string takeTheFormOfSwarmHint = "������� ����� ��� ��� �������������";
    [SerializeField] string pressQForVent = "������� Q ����� ���������� �� ����������";
    [SerializeField] string pressQForObstacle = "������� Q ��� ����������� �����������";
    [SerializeField] string hummerFormEnabled = "������� ����� ������";
    [SerializeField] string hummerHintPress2 = "������� 2 ��� ����������� � �����\n������� 1 ��� ����������� � ���\n������� E, ������ ����� ���,\n��� �������������� � ����������";
    [SerializeField] string hintCanBeCrushed = "���������� � ������ ��������� �������\n����� ������� �������";
    [SerializeField] string columnCanBeCrushed = "������� �� �������� ����� �������";
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
                    pressQForVent = "������� X ����� ���������� �� ����������";
                    pressQForObstacle = "������� X ��� ����������� �����������";
                    hummerHintPress2 = "������� RB ��� ����������� � �����\n������� LB ��� ����������� � ���\n������� Y, ������ ����� ���,\n��� �������������� � ����������";
                }
            }
        }
        else
        {
            Debug.Log("������� �� ���������.");
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