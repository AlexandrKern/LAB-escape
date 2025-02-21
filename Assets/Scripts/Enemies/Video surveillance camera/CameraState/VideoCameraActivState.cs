using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoCameraActivState : IEnemyState
{
    private VideoSurveillanceCamera _videoCamera;
    private float _timer;
    public VideoCameraActivState(VideoSurveillanceCamera videoCamera)
    {
        _videoCamera = videoCamera;
    }
    public void Enter()
    {
        if (_videoCamera.laserMove.laser != null)
        {
            _videoCamera.laserMove.laser.SetEndWidth(_videoCamera.eye.GetViewWidth());
        }
        _timer = 0;
        _videoCamera.laserMove.laser.isColorSwitching = true;
        _videoCamera.eye.StopAutoRotation();
        
    }

    public void Execute()
    {
        _videoCamera.laserMove.LookAt(_videoCamera.laserMove.TransformPlayer.position);
        _videoCamera.eye.AdjustViewToPoint(_videoCamera.laserMove.TransformPlayer.position);
        _timer += Time.deltaTime;
        if (_timer >= _videoCamera.eye.detectionTimer)
        {
            _videoCamera.spawnHunter.Spawn(_videoCamera._hunterSpawnPoint);
            _timer = 0;
        }
        if (!_videoCamera.eye.DetectPlayer())
        {
            _videoCamera.ChangeState(new VideoCameraObserveState(_videoCamera));
        }
    }

    public void Exit()
    {

    }
}
