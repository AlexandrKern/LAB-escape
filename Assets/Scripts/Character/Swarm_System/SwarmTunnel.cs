using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmTunnel : MonoBehaviour, IInteractableObstacle
{
    [SerializeField]
    private List<SwarmObstacle> swarmObstacles;

    private int _currentObstacleIndex;

    private void OnValidate()
    {
        if(swarmObstacles.Count > 1)
        {
            swarmObstacles[0].point1.gameObject.SetActive(true);
            swarmObstacles[0].point2.gameObject.SetActive(false);
            swarmObstacles[0].point2.position = 
                swarmObstacles[1].swarmForm.spriteRenderer.transform.position;
            for (int i = 1; i < swarmObstacles.Count - 1; i++)
            {
                swarmObstacles[i].point1.gameObject.SetActive(false);
                swarmObstacles[i].point1.position = 
                    swarmObstacles[i - 1].swarmForm.spriteRenderer.transform.position;
                swarmObstacles[i].point2.gameObject.SetActive(false);
                swarmObstacles[i].point2.position = 
                    swarmObstacles[i + 1].swarmForm.spriteRenderer.transform.position;
            }
            swarmObstacles[swarmObstacles.Count - 1].point1.gameObject.SetActive(false);
            swarmObstacles[swarmObstacles.Count - 1].point1.position = 
                swarmObstacles[swarmObstacles.Count - 2].swarmForm.spriteRenderer.transform.position;
            swarmObstacles[swarmObstacles.Count - 1].point2.gameObject.SetActive(true);
        }
        else if(swarmObstacles.Count == 1)
        {
            swarmObstacles[0].point1.gameObject.SetActive(true);
            swarmObstacles[0].point2.gameObject.SetActive(true);
        }
    }

    public async UniTask Interact()
    {
        //disable gravity
        Rigidbody2D swarmRigidBody = Swarm.Instance.GetComponent<Rigidbody2D>();
        float cacheGravity = swarmRigidBody.gravityScale;
        swarmRigidBody.gravityScale = 0;

        bool _translateIsOver = false;

        if (Vector3.SqrMagnitude(swarmObstacles[0].point1.position 
            - Swarm.Instance.transform.position)
            < 
            Vector3.SqrMagnitude(swarmObstacles[swarmObstacles.Count - 1].point2.position 
            - Swarm.Instance.transform.position))
        {
            TranslateForward();
        }
        else
        {
            TranslateBackward();
        }

        Swarm.Instance.EndTranslatinCallback += TmpFunc;
        await UniTask.WaitUntil(() => _translateIsOver);

        //enable gravity
        swarmRigidBody.gravityScale = cacheGravity;

        void TmpFunc()
        {
            _translateIsOver = true;
            Swarm.Instance.EndTranslatinCallback -= TmpFunc;
        }
    }

    private void TranslateForward()
    {
        _currentObstacleIndex = -1;
        MoveToNextObstacle();
    }

    private void MoveToNextObstacle()
    {
        _currentObstacleIndex++;
        if(_currentObstacleIndex >= swarmObstacles.Count)
        {
            return;
        }
       
        swarmObstacles[_currentObstacleIndex].TunnelTranslate(MoveToNextObstacle, isForward: true);
    }

    private void TranslateBackward()
    {
        _currentObstacleIndex = swarmObstacles.Count;
        MoveToPreviewObstacle();
    }

    private void MoveToPreviewObstacle()
    {
        _currentObstacleIndex--;
        if(_currentObstacleIndex < 0)
        {
            return;
        }
     
        swarmObstacles[_currentObstacleIndex].TunnelTranslate(MoveToPreviewObstacle, isForward: false);
    }
}
