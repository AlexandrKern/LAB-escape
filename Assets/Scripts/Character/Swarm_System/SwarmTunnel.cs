using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmTunnel : MonoBehaviour, IInteractableObstacle
{
    [SerializeField]
    private List<SwarmObstacle> swarmObstacles;

    [SerializeField]
    private GameObject target;

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
        Rigidbody2D swarmRigidBody = Swarm.Instance.GetComponent<Rigidbody2D>();
        float cacheGravity = swarmRigidBody.gravityScale;
        swarmRigidBody.gravityScale = 0;

        if (Vector3.SqrMagnitude(swarmObstacles[0].point1.position 
            - Swarm.Instance.transform.position)
            < 
            Vector3.SqrMagnitude(swarmObstacles[swarmObstacles.Count - 1].point2.position 
            - Swarm.Instance.transform.position))
        {
            await TranslateForward();
        }
        else
        {
            await TranslateBackward();
        }

        swarmRigidBody.gravityScale = cacheGravity;
    }

    private async UniTask TranslateForward()
    {
        for( int i = 0; i < swarmObstacles.Count; i++)
        {
            target.transform.position = swarmObstacles[i].swarmForm.spriteRenderer.transform.position;
            await swarmObstacles[i].Interact();
        }
    }

    private async UniTask TranslateBackward()
    {
        for( int i = swarmObstacles.Count - 1; i >= 0; i--)
        {
            await swarmObstacles[i].Interact();
        }
    }
}
