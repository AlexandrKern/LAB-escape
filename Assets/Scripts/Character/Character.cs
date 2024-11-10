using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

[RequireComponent(typeof(Swarm))]
[RequireComponent(typeof(MoveController))]
[RequireComponent(typeof(InteractController))]
[RequireComponent(typeof(PunchController))]
[RequireComponent(typeof(SwarmInputHandler))]
public class Character : MonoBehaviour
{
    [SerializeField] private DetectionIndicator detectionIndicator;

    private StateMachine _stateMachine;
    [HideInInspector]
    public Swarm swarm;
    [HideInInspector]
    public MoveController moveController;
    [HideInInspector]
    public InteractController interactController;
    [HideInInspector]
    public PunchController punchController;
    [HideInInspector]
    public SwarmInputHandler inputHandler;
    [HideInInspector]
    public HintController hintController;

    private HashSet<EnemyEye> _observersEnemyes = new();

    public static Character Instance { get; private set; }

    public float InputHorizontal 
    {
        set
        {
            _stateMachine.InputHorizontal = value;
        } 
    }
    public float InputVertical 
    {
        set
        {
            _stateMachine.InputVertical = value;
        }
    }

    public void OnDetection(EnemyEye observer)
    {
        _observersEnemyes.Add(observer);
        detectionIndicator.SetDetectionState(true);
    }

    public void OnMiss(EnemyEye observer)
    {
        _observersEnemyes.Remove(observer);
        if(_observersEnemyes.Count <= 0)
        {
            detectionIndicator.SetDetectionState(false);
        }
    }

    public void QInteract()
    {
        _stateMachine.QInteract();
        hintController.CleanTheHint();
    }

    public void EInteract()
    {
        _stateMachine.EInteract();
    }

    public void ChangeForm(FormType form)
    {
        if (form != FormType.Base)
        {
            swarm.DisableForms();
            swarm.EnableFormObj((int)form); // включает объект формы, это нужно чтобы постоянно не держать их коллайдеры в поиске коллизии
                                            // и работы класса BottleneckDetector
        }

        else
            swarm.DisableForms(); // если возвращаемся в рой, то выключаем объекты других форм
        
        _stateMachine.EnterState(form);
    }

    void Awake()
    {
        swarm = GetComponent<Swarm>();
        moveController = GetComponent<MoveController>();
        interactController = GetComponent<InteractController>();
        _stateMachine = new StateMachine(this, FormType.Base);
        punchController = GetComponent<PunchController>();
        inputHandler = GetComponent<SwarmInputHandler>();
        hintController = GetComponent<HintController>();
    }

    private void Start()
    {
        UpdateStates();
        Instance = this;
    }

    public void UpdateStates()
    {
        _stateMachine.UpdateStatesCollection();
    }

    public FormType GetCharacterForm()
    {
        return _stateMachine.GetForm();
    }

    public void StateMachineUpdater()
    {
        _stateMachine.Update();
    }
}
