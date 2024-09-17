using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;


public partial class Swarm : MonoBehaviour
{
    public const int PointCounts = 2000;
    [SerializeReference][SerializeField] private List<SwarmFormBase> forms;
    [Space]
    [SerializeField, Range(100, 1000)] private int maxNumberOfUnits = 500;
    [SerializeField, Range(0, 1000)] private int minNumberOfUnits = 100;
    [Space]
    [SerializeField] private GameObject unitPrefab;
    [SerializeField] private GameObject pointPrefab;
    [Space]
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [Space]
    [SerializeField] private float minTranslationSpeed;
    [SerializeField] private float maxTranslationSpeed;
    [Space]
    [SerializeField] private float minSize;
    [SerializeField] private float maxSize;
    [Space]
    [SerializeField] private float maxDistance = 1;
    [Space]

    private List<Unit> _units = new List<Unit>();
    private Texture2D _texture;
    private List<DestinationPoint> _tempPoints = new List<DestinationPoint>();

    private enum SwarmPhase { Static, MoveToTempObject, MoveFromTempObject }
    private SwarmPhase _swarmPhase = SwarmPhase.Static;

    private Transform _newTransform;
    private Texture2D texture;
    private Transform _transform;
    private int _currentFormIndex = 0;
    private int numberOfUnits = 500;
    private int tempSortingOrder;

    public float MinSpeed { get => minSpeed; private set => minSpeed = value; }
    public float MaxSpeed { get => maxSpeed; private set => maxSpeed = value; }
    public float MinSize { get => minSize; private set => minSize = value; }
    public float MaxSize { get => maxSize; private set => maxSize = value; }
    public float MinTranslationSpeed { get => minTranslationSpeed; private set => minTranslationSpeed = value; }
    public float MaxTranslationSpeed { get => maxTranslationSpeed; private set => maxTranslationSpeed = value; }

    public event System.Action EndTranslatinCallback; //Callback завершения перехода. Данный Callback стоит использовать для возвращения пользователю контродля после перехода.

    public void SetFormIndex(int index)//Принять форму с соответствующим индексом из массива forms.
    {
        _currentFormIndex = index;
    }

    public void SetUnitCount(int count)//Задает колличевство найнитов.
    {
        numberOfUnits = math.clamp(count, minNumberOfUnits, maxNumberOfUnits);
    }

    public void SetSortingOrder(int order)//Задать sortingOrder для всех юнитов.
    {
        for (int i = 0; i < _units.Count; i++)
        {
            _units[i].SetSortingOrder(order);
        }
    }

    public void Translate(List<DestinationPoint> destinationPoints, Transform newTransform)//Выполнить переход через форму отверстия.
    {
        _tempPoints = destinationPoints;
        _swarmPhase = SwarmPhase.MoveToTempObject;
        _newTransform = newTransform;
    }

    public void Translate(List<DestinationPoint> destinationPoints, Transform newTransform ,int sortingOrder)//Выполнить переход через форму отверстия со сменой SortingOrder.
    {
        _tempPoints = destinationPoints;
        _swarmPhase = SwarmPhase.MoveToTempObject;
        _newTransform = newTransform;
        tempSortingOrder = sortingOrder;
    }

    private DestinationPoint GetRandomEmptyPoint(Unit unit)
    {
        switch (_swarmPhase)
        {
            case SwarmPhase.Static:
                return GetEmptyPoint(forms[_currentFormIndex].GetDestenationPoints(), unit);
            case SwarmPhase.MoveToTempObject:
                if (AllPointsIsFree(forms[_currentFormIndex].GetDestenationPoints()))
                {
                    _swarmPhase = SwarmPhase.MoveFromTempObject;
                    SetSortingOrder(tempSortingOrder);
                    /*
                    for (int i = 0; i < _units.Count; i++)
                    {
                        _units[i].Transform.position += _transform.position - _newTransform.position;
                    }
                    */
                    _transform.position = _newTransform.position;
                }
                return GetEmptyPoint(_tempPoints);
            case SwarmPhase.MoveFromTempObject:
                if (AllPointsIsFree(_tempPoints))
                {
                    _swarmPhase = SwarmPhase.Static;
                    EndTranslatinCallback?.Invoke();
                }
                return GetEmptyPoint(forms[_currentFormIndex].GetDestenationPoints());
            default:
                return null;
        }
    }

    private void Start()
    {
        _transform = transform;
        //GeneratePoints();
        GenerateUnits();
    }

    //private void Update()
    //{
    //    SwarmUpdater();
    //}

    public void SwarmUpdater()
    {
        for (int i = 0; i < _units.Count; i++)
        {
            bool visible = true;
            if (i > numberOfUnits)
            {
                visible = false;
            }
            _units[i].UpdateUnit(this, _swarmPhase != SwarmPhase.Static, visible);
        }
    }

    private void GenerateUnits()
    {
        for (int i = 0; i < maxNumberOfUnits; i++)
        {
            Transform ut = Instantiate(unitPrefab, forms[_currentFormIndex].GetDestenationPoints()[i].Transform.position, Quaternion.identity, null).transform;
            _units.Add(new Unit(ut, forms[_currentFormIndex].GetDestenationPoints()[i],ut.GetComponent<SpriteRenderer>()));
        }
    }

    private bool AllPointsIsFree(List<DestinationPoint> points)
    {
        for (int i = 0; i < points.Count; i++)
        {
            if (!points[i].IsFree)
            {
                return false;
            }
        }
        return true;
    }
    
    private DestinationPoint GetEmptyPoint(List<DestinationPoint> points, Unit unit)
    {

        for (int i = 0; i < 100; i++)
        {
            int index = UnityEngine.Random.Range(0, points.Count);
            if (points[index].IsFree && (Vector3.Distance(points[index].Transform.position, unit.Transform.position) < maxDistance || i==99))
            {
                points[index].IsFree = false;
                return points[index];
            }
        }
        return points[0];
    }
    
    private DestinationPoint GetEmptyPoint(List<DestinationPoint> points)
    {
        while (true)
        {
            int index = UnityEngine.Random.Range(0, points.Count);
            if (points[index].IsFree)
            {
                points[index].IsFree = false;
                return points[index];
            }
        }
    }
  
}
