using TMPro;
using UnityEngine;
using DG.Tweening;

public class HintController : MonoBehaviour
{
    [SerializeField] TextMeshPro hintText;
    [SerializeField] string takeTheFormOfSwarmHint = "Примите форму роя для взаимодейстия";
    [SerializeField] string pressQForVent = "Нажмите Q чтобы пробраться по вентиляции";
    [SerializeField] string pressQForObstacle = "Нажмите Q для преодоления препятствия";
    [SerializeField] string hummerFormEnabled = "Открыта форма молота";

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

    private void ShowHint(string hint)
    {
        hintText.DOFade(0, 0);
        hintText.text = hint;
        hintText.DOFade(1, 0.5f).OnComplete(() =>
        {
            DOVirtual.DelayedCall(3f, CleanTheHint);
        });
    }

    public void HintTakeTheFormOfSwarm()
    {
        ShowHint(takeTheFormOfSwarmHint);
    }

    public void HintPressQForObstacle()
    {
        ShowHint(pressQForObstacle);
    }

    public void HintPressQForVentilation()
    {
        ShowHint(pressQForVent);
    }

    public void HintHammerFormEnabled()
    {
        ShowHint(hummerFormEnabled);
    }

    private void OnDestroy()
    {
        DOTween.KillAll();
    }
}