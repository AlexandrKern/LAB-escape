using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Swarm))]
[RequireComponent(typeof(MoveController))]
public class SwarmInputHandler : MonoBehaviour
{
    [SerializeField]
    private List<KeyCode> formsKeyboardKeys;

    private Swarm _swarm;
    private MoveController _moveController;

    private void Awake()
    {
        _swarm = GetComponent<Swarm>();
        _moveController = GetComponent<MoveController>();
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

        _moveController.InputHorizontal = Input.GetAxis("Horizontal");
    }
}
