using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class SwarmInputHandler : MonoBehaviour
{
    [Serializable]
    private struct FormKeyboardKey
    {
        public KeyCode Key;
        public FormType Form;
    }

    [SerializeField]
    private List<FormKeyboardKey> formsKeyboardKeys;

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
            if (Input.GetKeyDown(formsKeyboardKeys[i].Key))
            {
                _character.ChangeForm(formsKeyboardKeys[i].Form);
            }
       }

        _character.InputHorizontal = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(interactKeyboardKey))
        {
            _character.Interact();
        }
    }
}
