using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoSurveillanceCamera : MonoBehaviour
{
    private IEnemyState currentState;

    [HideInInspector] public EnemyEye eye;
    public CameraLaserMove laserMove;


    private void Start()
    {
        eye = GetComponent<EnemyEye>();
        ChangeState(new VideoCameraObserveState(this));
    }


    private void Update()
    {
        if (currentState != null)
        {
            currentState.Execute();
        }
    }

    public void ChangeState(IEnemyState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Enter();
    }

    
}
