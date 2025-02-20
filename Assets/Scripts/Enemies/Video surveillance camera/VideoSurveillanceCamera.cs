using UnityEngine;

public class VideoSurveillanceCamera : MonoBehaviour
{
    private IEnemyState currentState;

    [HideInInspector] public EnemyEye eye;
    public CameraLaserMove laserMove;

    private bool isOff = false;

    private void Start()
    {
        eye = GetComponent<EnemyEye>();
        ChangeState(new VideoCameraObserveState(this));
    }


    private void Update()
    {
        if (isOff) return;
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


    public void OffCamera()
    {
        isOff = true;
        eye.StopAutoRotation();
        laserMove.laser.Stop();
    }

    private void OnCamera()
    {
        isOff = false;
        eye.StartAutoRotation();
        laserMove.laser.Play();
    }


    
}
