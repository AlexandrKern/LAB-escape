using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SwarmForm))]
public class SwarmObstacle : MonoBehaviour
{
    [SerializeField] private Transform point1;
    [SerializeField] private int point1SortingOrder;
    [Space]
    [SerializeField] private Transform point2;
    [SerializeField] private int point2SortingOrder;
    [Space]
    [SerializeField] private bool setSortingOrder;

    private Swarm swarm;
    private SwarmForm swarmForm;

    void Start()
    {
        swarm = FindObjectOfType<Swarm>();
        swarmForm = GetComponent<SwarmForm>();
    }

    public void Translate()
    {
        Transform t = swarm.transform;
        if (Vector3.Distance(t.position, point1.position) > Vector3.Distance(t.position, point2.position))
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
        else
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
    }
}

