using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour, IInteractable
{
    [SerializeField]
    private float VerticalSpeed = 5f;
    [SerializeField]
    private float HighestPointOffset = 5f;
    [SerializeField]
    private float LowestPointOffset = 5f;
    [SerializeField]
    private KeyCode JumpDownKey = KeyCode.E;

    public async UniTask Interact()
    {
        Rigidbody2D swarmRigidBody = Swarm.Instance.GetComponent<Rigidbody2D>();
        bool kinematicCache = swarmRigidBody.isKinematic;
        swarmRigidBody.isKinematic = true;
        swarmRigidBody.velocity = Vector2.zero;

        await MoveToLadder();

        float highestY = transform.position.y + HighestPointOffset;
        float lowestY = transform.position.y + LowestPointOffset;

        while (swarmRigidBody.position.y <= highestY && swarmRigidBody.position.y >= lowestY)
        {
            if(Input.GetKeyDown(JumpDownKey))
            {
                break;
            }
            swarmRigidBody.position =
                swarmRigidBody.position + Vector2.up * VerticalSpeed * 
                Input.GetAxis("Vertical") * Time.deltaTime;
            await UniTask.Yield(destroyCancellationToken);
        }

        swarmRigidBody.isKinematic = kinematicCache;
    }

    private async UniTask MoveToLadder()
    {
        Transform playerTransform = Swarm.Instance.transform;

        Vector3 target = new Vector3
        {
            x = transform.position.x,
            y = Mathf.Clamp(playerTransform.position.y,
                transform.position.y + LowestPointOffset,
                transform.position.y + HighestPointOffset),
            z = playerTransform.position.z,
        };

        while(playerTransform.position != target)
        {
            playerTransform.position = Vector3.MoveTowards(playerTransform.position, target,
                VerticalSpeed * Time.deltaTime);
            await UniTask.Yield(destroyCancellationToken);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + Vector3.up * HighestPointOffset, 0.1f);
        Gizmos.DrawSphere(transform.position + Vector3.up * LowestPointOffset, 0.1f);
    }
}
