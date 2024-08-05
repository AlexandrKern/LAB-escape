using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Swarm))]
public class SwarmInputHandler : MonoBehaviour
{
    [SerializeField]
    private List<KeyCode> formsKeyboardKeys;

    private Swarm _swarm;

    private void Awake()
    {
        _swarm = GetComponent<Swarm>();
    }

    void Update()
    {
       for(int i = 0; i < formsKeyboardKeys.Count; i++)
       {
            //TODO: add form accessibility and array overrun checking
            if (Input.GetKeyDown(formsKeyboardKeys[i]))
            {
                _swarm.SetFormIndex(i);
            }
       }
    }
}
