using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Swarm))]
[RequireComponent(typeof(MoveController))]
[RequireComponent(typeof(InteractController))]
public class SwarmInputHandler : MonoBehaviour
{
    [SerializeField]
    private List<KeyCode> formsKeyboardKeys;

    [SerializeField]
    private KeyCode interactKeyboardKey;

    private Swarm _swarm;
    private MoveController _moveController;
    private InteractController _interactController;

    private void Awake()
    {
        _swarm = GetComponent<Swarm>();
        _moveController = GetComponent<MoveController>();
        _interactController = GetComponent<InteractController>();
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

        if (Input.GetKeyDown(interactKeyboardKey))
        {
            _interactController.Interact();
        }
    }
}
