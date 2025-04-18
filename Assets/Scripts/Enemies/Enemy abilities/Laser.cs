using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer), (typeof(MeshFilter)))]
public class Laser : MonoBehaviour
{
    [SerializeField] private float _startWidth;
    public float endWidth;
    [SerializeField] private Transform _rayStart;
    [SerializeField] private Transform _rayEnd;
    [SerializeField] private ContactFilter2D _contactFilter;
    [SerializeField] private int _rayCount = 20;
    [HideInInspector] public float maxDistanceVisible = 5;
    [HideInInspector] public float maxDistance = 5;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _playerVisibleColor;
    [SerializeField] private float _colorLerpTime = 0.5f;
    [SerializeField] private float _blinkInterval = 0.5f; // �������� �������
    private MeshFilter _meshFilter;
    private MeshRenderer _meshRenderer;
    private Transform _transform;
    [HideInInspector] public bool isColorSwitching;
    [HideInInspector] public bool isPlayerVisible;

    private bool _isBlinking;
    private Coroutine _blinkCoroutine;

    List<int> _triangles = new List<int>();
    List<Vector3> _vertices = new List<Vector3>();
    List<Vector3> _endRayPoint = new List<Vector3>();
    List<Vector2> _UVs = new List<Vector2>();
    List<Vector2> _dir = new List<Vector2>();
    List<Vector2> _origin = new List<Vector2>();

    private float _colorLerpT;

    private bool _isOff = false;


    void Start()
    {
        _transform = transform;
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshFilter = GetComponent<MeshFilter>();
        _meshFilter.mesh = new Mesh();
    }

    private void OnEnable()
    {
        _transform = transform;
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshFilter = GetComponent<MeshFilter>();
        _meshFilter.mesh = new Mesh();
    }

    private void Update()
    {
        if(_isOff)return;
        _transform.rotation = Quaternion.identity;
    }

    private void FixedUpdate()
    {
        if (_isOff) return;
        _transform.rotation = Quaternion.identity;
        _rayStart.forward = _rayEnd.position - _rayStart.position;

        _triangles.Clear();
        _vertices.Clear();
        _endRayPoint.Clear();
        _UVs.Clear();
        _dir.Clear();
        _origin.Clear();

        for (int i = 0; i < _rayCount; i++)
        {
            if (_rayStart.position.x == _rayEnd.position.x)
            {
                _rayEnd.position += Vector3.right * 0.001f;
                _rayStart.forward = _rayEnd.position - _rayStart.position;
            }
            Debug.DrawRay(_rayStart.position, _rayStart.up);
            Debug.DrawRay(_rayStart.position, -_rayStart.up);
            _origin.Add(Vector2.Lerp(_rayStart.position - _rayStart.up * _startWidth / 2f, _rayStart.position + _rayStart.up * _startWidth / 2, (float)i / _rayCount));
            _dir.Add((Vector2.Lerp(_rayEnd.position - _rayStart.up * endWidth / 2f, _rayEnd.position + _rayStart.up * endWidth / 2f, (float)i / _rayCount) - _origin[i]).normalized);
            List<RaycastHit2D> hits = new List<RaycastHit2D>();

            isPlayerVisible = false;

            if (0 < Physics2D.Raycast(_origin[i], _dir[i], _contactFilter, hits, maxDistanceVisible))
            {
                _endRayPoint.Add(hits[0].point - _origin[i]);
                for (int j = 0; j < hits.Count; j++)
                {
                    if (hits[j].collider.CompareTag("Player"))
                    {
                        isPlayerVisible = true;
                        break;
                    }
                }
                hits.Clear();
            }
            else
            {
                _endRayPoint.Add(_dir[i] * maxDistanceVisible);
            }
        }

        _vertices.Add(_rayStart.localPosition - _rayStart.up * _startWidth / 2);
        _vertices.Add(_endRayPoint[0]);
        for (int i = 1; i < _endRayPoint.Count; i++)
        {
            _vertices.Add(_endRayPoint[i]);
            _triangles.Add(0);
            _triangles.Add(_vertices.Count - 1);
            _triangles.Add(_vertices.Count - 2);
        }

        _vertices.Add(_rayStart.localPosition + _rayStart.up * _startWidth / 2);
        _triangles.Add(_endRayPoint.Count);
        _triangles.Add(0);
        _triangles.Add(_endRayPoint.Count + 1);

        _UVs.Add(Vector2.up);

        for (int i = 0; i < _endRayPoint.Count; i++)
        {
            float yUV = maxDistanceVisible / Vector2.SqrMagnitude(_endRayPoint[i]);
            _UVs.Add(new Vector2((float)i / _endRayPoint.Count, yUV));
        }

        _UVs.Add(Vector2.one);
        _meshFilter.mesh.vertices = _vertices.ToArray();
        _meshFilter.mesh.uv = _UVs.ToArray();
        _meshFilter.mesh.triangles = _triangles.ToArray();
        _meshFilter.mesh.RecalculateBounds();

        if (isColorSwitching)
        {
            _colorLerpT = math.clamp(_colorLerpT + Time.fixedDeltaTime / _colorLerpTime, 0, 1);
        }
        else
        {
            _colorLerpT = math.clamp(_colorLerpT - Time.fixedDeltaTime / _colorLerpTime, 0, 1);
        }
        _meshRenderer.material.SetColor("_Color", Color.Lerp(_defaultColor, _playerVisibleColor, _colorLerpT));
    }

    public void StartBlinking()
    {
        if (_isBlinking) return;
        _isBlinking = true;
        _blinkCoroutine = StartCoroutine(BlinkRoutine());
    }

    public void StopBlinking()
    {
        if (_blinkCoroutine != null)
        {
            _isBlinking = false;
            StopCoroutine(_blinkCoroutine);
            _blinkCoroutine = null;
            _meshRenderer.material.SetColor("_Color", _defaultColor); // ���������� ����� � ��������� �����
        }
    }

    private IEnumerator BlinkRoutine()
    {
        while (_isBlinking)
        {
            _meshRenderer.material.SetColor("_Color", _playerVisibleColor);
            yield return new WaitForSeconds(_blinkInterval);
            _meshRenderer.material.SetColor("_Color", _defaultColor);
            yield return new WaitForSeconds(_blinkInterval);
        }
    }


    public void SetEndWidth(float endWidth)
    {
        this.endWidth = endWidth;
    }

    public void Stop()
    {
        _isOff = true;
    }
    public void Play()
    {
        _isOff = false;
    }

}




