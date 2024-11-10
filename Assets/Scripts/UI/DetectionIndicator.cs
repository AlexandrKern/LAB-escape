using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionIndicator : MonoBehaviour
{
    [SerializeField] GameObject detectionIndicator;
    [SerializeField] GameObject undetectionIndicator;

    void Start()
    {
        SetDetectionState(false);
    }

    public void SetDetectionState(bool isDetected)
    {
        detectionIndicator.SetActive(isDetected);
        undetectionIndicator.SetActive(!isDetected);
    }

}
