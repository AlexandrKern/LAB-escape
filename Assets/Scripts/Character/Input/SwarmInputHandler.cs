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
    private KeyCode interactQKeyboardKey;

    [SerializeField]
    private KeyCode interactEKeyboardKey;

    private Character _character;
    private Swarm _swarm;

    private bool _isEnableInput = true;
    public bool IsEnableInput
    {
        get => _isEnableInput;
        set
        {
            _isEnableInput = value;
            if(!value)
            {
                OnDisableInput();
            }
        }
    }

    private void Awake()
    {
        _character = GetComponent<Character>();
        _swarm = GetComponent<Swarm>();
    }

    void Update()
    {
        if(IsEnableInput)
        {
            HandleInput();
        }

        _character.StateMachineUpdater();
        _swarm.SwarmUpdater(); // ��� ArgumentOutOfRangeException
    }

    private void HandleInput()
    {
        for (int i = 0; i < formsKeyboardKeys.Count; i++)
        {
            if (Input.GetKeyDown(formsKeyboardKeys[i].Key))
            {
                //Debug.Log(i);
                _character.ChangeForm(formsKeyboardKeys[i].Form);
            }
        }

        if (Input.GetKeyDown(KeyCode.JoystickButton4))
        {
            _character.ChangeForm(formsKeyboardKeys[0].Form); // ����� ����� �� ������� ���������� ������� �������� � ��������,
                                                              // �� ����� ������� ��� ������
        }

        if (Input.GetKeyDown(KeyCode.JoystickButton5))
        {
            _character.ChangeForm(formsKeyboardKeys[1].Form); // ����� ���� ����� �� ������� ���������� ������� �������� � ��������,
                                                              // �� ����� ������� ��� ������
        }


        _character.InputHorizontal = Input.GetAxis("Horizontal");
        _character.InputVertical = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(interactQKeyboardKey) || Input.GetKeyDown(KeyCode.JoystickButton2))
        {
            _character.QInteract();
        }

        if (Input.GetKeyDown(interactEKeyboardKey) || Input.GetKeyDown(KeyCode.JoystickButton3))
        {
            _character.EInteract();
        }
    }

    private void OnDisableInput()
    {
        _character.InputHorizontal = 0f;
    }
}
