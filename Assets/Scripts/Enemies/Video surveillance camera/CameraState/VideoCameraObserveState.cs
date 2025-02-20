public class VideoCameraObserveState : IEnemyState
{
    private VideoSurveillanceCamera _videoCamera;
    public VideoCameraObserveState(VideoSurveillanceCamera videoCamera)
    {
        _videoCamera = videoCamera;
    }
    public void Enter()
    {
        if (_videoCamera.laserMove.laser != null)
        {
            _videoCamera.laserMove.laser.SetEndWidth(_videoCamera.eye.GetViewWidth());
            _videoCamera.laserMove.laser.isColorSwitching = false;
        }
        
        _videoCamera.eye.StartAutoRotation();
    }

    public void Execute()
    {
       _videoCamera.laserMove.LookAt(_videoCamera.eye.GetViewCenterPoint());
        if (_videoCamera.eye.DetectPlayer())
        {
            _videoCamera.ChangeState(new VideoCameraActivState(_videoCamera));
        }
    }

    public void Exit()
    {
        
    }
}
