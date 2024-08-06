using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class SwarmInputHandler : MonoBehaviour
{
    [SerializeField]
    private List<KeyCode> formsKeyboardKeys;

    [SerializeField]
    private KeyCode interactKeyboardKey;

    private Character _character;

    private void Awake()
    {
        _character = GetComponent<Character>();
    }

    void Update()
    {
       for(int i = 0; i < formsKeyboardKeys.Count; i++)
       {
            if (Input.GetKeyDown(formsKeyboardKeys[i]))
            {
                _character.ChangeForm(i);
            }
       }

        _character.InputHorizontal = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(interactKeyboardKey))
        {
            _character.Interact();
        }
    }
}
