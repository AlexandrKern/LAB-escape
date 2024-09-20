using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SwarmForm))]
public class SwarmObstacle : MonoBehaviour, IInteractableObstacle
{
    [SerializeField] public Transform point1;
    [SerializeField] private int point1SortingOrder;
    [Space]
    [SerializeField] public Transform point2;
    [SerializeField] private int point2SortingOrder;
    [Space]
    [SerializeField] private bool setSortingOrder;

    private Swarm swarm => Swarm.Instance;

    [SerializeField]
    [HideInInspector]
    public SwarmForm swarmForm;

    /*
    void Start()
    {
        //swarm = FindObjectOfType<Swarm>();
        swarmForm = GetComponent<SwarmForm>();
    }
    */

    private void OnValidate()
    {
        swarmForm = GetComponent<SwarmForm>();
    }

    public void Translate()
    {
        Transform t = swarm.transform;
        if (Vector3.Distance(t.position, point1.position) > Vector3.Distance(t.position, point2.position))
        {
            TranslateBackward();
        }
        else
        {
            TranslateForward();
        }
    }

    public void TranslateBackward()
    {
        if (setSortingOrder)
        {
            swarm.Translate(swarmForm.GetDestenationPoints(), point1, point1SortingOrder);
        }
        else
        {
            swarm.Translate(swarmForm.GetDestenationPoints(), point1);
        }
    }

    public void TranslateForward()
    {
        if (setSortingOrder)
        {
            swarm.Translate(swarmForm.GetDestenationPoints(), point2, point2SortingOrder);
        }
        else
        {
            swarm.Translate(swarmForm.GetDestenationPoints(), point2);
        }
    }

    public void TunnelTranslate(System.Action OnReachingObstacle, bool isForward)
    {
        swarm.OnMoveFromTempObject += TmpFunc;
        if(isForward)
        {
            TranslateForward();
        }
        else
        {
            TranslateBackward();
        }

        void TmpFunc()
        {
            OnReachingObstacle?.Invoke();
            swarm.OnMoveFromTempObject -= TmpFunc;
        }
    }

    public async UniTask Interact()
    {
        bool translateIsOver = false;
        swarm.EndTranslatinCallback += TmpFunc;
        Translate();
        await UniTask.WaitUntil(()=>translateIsOver);
        swarm.EndTranslatinCallback -= TmpFunc;

        void TmpFunc()
        {
            translateIsOver = true;
        }
    }
}

