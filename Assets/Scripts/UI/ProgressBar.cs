using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image bar;

    public void SetFillMethod(Image.FillMethod method)
    {
        bar.fillMethod = method;
    }

    public void SetFillAmount(float value)
    {
        bar.fillAmount = value;
    }
}
