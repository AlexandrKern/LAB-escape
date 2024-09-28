using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HintController : MonoBehaviour
{
    [SerializeField] TextMeshPro _hintText;
    public void SetHintTextRotation(float value)
    {
        Vector3 hintTextRotation = _hintText.transform.rotation.eulerAngles;
        hintTextRotation.y = value;
        _hintText.transform.rotation = Quaternion.Euler(hintTextRotation);
    }
}