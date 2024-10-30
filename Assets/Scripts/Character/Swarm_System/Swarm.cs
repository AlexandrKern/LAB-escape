using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public partial class Swarm : MonoBehaviour
{
    public const int PointCounts = 2000;
    [SerializeReference][SerializeField] private List<SwarmFormBase> forms; // порядок форм в массиве должен соответствовать порядку форм в энаме FormType!
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
    private List<DestinationPoint> _lastPoints = null;

    private enum SwarmPhase { Static, MoveToTempObject, MoveFromTempObject }
    private SwarmPhase _swarmPhase = SwarmPhase.Static;

    private Transform _newTransform;
    private Texture2D texture;
    private Transform _transform;
    private int _currentFormIndex = 0;
    private int numberOfUnits = 500;
    private int tempSortingOrder;
    private Transform _unitsRoot;

    public float MinSpeed { get => minSpeed; private set => minSpeed = value; }
    public float MaxSpeed { get => maxSpeed; private set => maxSpeed = value; }
    public float MinSize { get => minSize; private set => minSize = value; }
    public float MaxSize { get => maxSize; private set => maxSize = value; }
    public float MinTranslationSpeed { get => minTranslationSpeed; private set => minTranslationSpeed = value; }
    public float MaxTranslationSpeed { get => maxTranslationSpeed; private set => maxTranslationSpeed = value; }

    public event System.Action EndTranslatinCallback; //Callback завершения перехода. Данный Callback стоит использовать для возвращения пользователю контродля после перехода.
    public event System.Action OnMoveFromTempObject;

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
        if(_lastPoints == null)
        {
            _lastPoints = forms[_currentFormIndex].GetDestenationPoints();
        }
        else
        {
            _lastPoints = _tempPoints;
        }
        _tempPoints = destinationPoints;
        _swarmPhase = SwarmPhase.MoveToTempObject;
        _newTransform = newTransform;
    }

    public void Translate(List<DestinationPoint> destinationPoints, Transform newTransform ,int sortingOrder)//Выполнить переход через форму отверстия со сменой SortingOrder.
    {
        if (_lastPoints == null)
        {
            _lastPoints = forms[_currentFormIndex].GetDestenationPoints();
        }
        else
        {
            _lastPoints = _tempPoints;
        }
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
                if (AllPointsIsFree(_lastPoints))
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
                if (OnMoveFromTempObject != null)
                {
                    OnMoveFromTempObject.Invoke();
                    return GetEmptyPoint(_tempPoints);
                }
                if (AllPointsIsFree(_tempPoints))
                {
                    _swarmPhase = SwarmPhase.Static;
                    EndTranslatinCallback?.Invoke();
                    _lastPoints = null;
                }
                return GetEmptyPoint(forms[_currentFormIndex].GetDestenationPoints());
            default:
                return null;
        }
    }

    private void Start()
    {
        DOTween.SetTweensCapacity(500, 50);
        _transform = transform;
        //GeneratePoints();
        _unitsRoot = new GameObject("Units").transform;
        GenerateUnits();
        DisableForms();
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

    List <SpriteRenderer> beatles; 

    private void GenerateUnits()
    {
        beatles = new List<SpriteRenderer>();
        for (int i = 0; i < maxNumberOfUnits; i++)
        {
            Transform ut = Instantiate(unitPrefab, forms[_currentFormIndex].GetDestenationPoints()[i].Transform.position, Quaternion.identity, _unitsRoot).transform;
            SpriteRenderer uts = ut.GetComponent<SpriteRenderer>();
            _units.Add(new Unit(ut, forms[_currentFormIndex].GetDestenationPoints()[i],ut.GetComponent<SpriteRenderer>()));
            beatles.Add(uts); 
        }
    }

    private int lastIndexOfForm;


    public void SwarmFade(int indexOfForm)
    {
        for (int i = 0; i < beatles.Count; i++)
        {
            DOTween.Kill(beatles[i]);
            beatles[i].DOFade(0, 1.5f);
        }
        lastIndexOfForm = indexOfForm;
        var tmp = forms[indexOfForm].GetComponent<SpriteRenderer>();
        DOTween.Kill(tmp);
        tmp.DOFade(1, 1f).SetDelay(0.5f);
    }

    public void SwarmFadeBack()
    {
        var tmp = forms[lastIndexOfForm].GetComponent<SpriteRenderer>();
        DOTween.Kill(tmp);
        tmp.DOFade(0, 0f);
        for (int i = 0; i < beatles.Count; i++)
        {
            DOTween.Kill(beatles[i]);
            beatles[i].DOFade(1, 0f);
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

    public void EnableFormObj(int index)
    {
        forms[index].gameObject.SetActive(true);
    }

    public void DisableForms()
    {
        for (int i = 1; i < forms.Count; i++) 
        {
            forms[i].gameObject.SetActive(false); // выключаем все объекты форм кроме роя 
        }
    }
}
