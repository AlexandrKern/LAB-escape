using TMPro;
using UnityEngine;
using DG.Tweening;

public class HintController : MonoBehaviour
{
    [SerializeField] TextMeshPro hintText;
    [SerializeField] string takeTheFormOfSwarmHint = "Примите форму роя для взаимодейстия";
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

    public void HintTakeTheFormOfSwarm()
    {
        hintText.DOFade(1, 0.5f);
        hintText.text = takeTheFormOfSwarmHint;
        hintText.DOFade(1, 0.5f).OnComplete(() =>
        {
            DOVirtual.DelayedCall(3f, CleanTheHint);
        });
    }

    private void OnDestroy()
    {
        DOTween.KillAll();
    }
}