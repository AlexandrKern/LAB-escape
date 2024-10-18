using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer), (typeof(MeshFilter)))]
public class Laser : MonoBehaviour
{
    [SerializeField] private float _startWidth;
    public float endWidth;
    private float _startEndWidth;
    [SerializeField] private Transform _rayStart;
    [SerializeField] private Transform _rayEnd;
    [SerializeField] private ContactFilter2D _contactFilter;
    [SerializeField] private int _rayCount = 20;
    public float maxDistance = 5;
    private float _startMaxDistance;
    [SerializeField] private Collider2D _playerCollider;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _playerVisibleColor;
    [SerializeField] private float _colorLerpTime = 0.5f;
    private MeshFilter _meshFilter;
    private MeshRenderer _meshRenderer;
    private Transform _transform;
    private Sprite _sprite;
    public bool playerVisible;

    List<int> _triangles = new List<int>();
    List<Vector3> _vertices = new List<Vector3>();
    List<Vector3> _endRayPoint = new List<Vector3>();
    List<Vector2> _UVs = new List<Vector2>();
    List<Vector2> _dir = new List<Vector2>();
    List<Vector2> _origin = new List<Vector2>();

    private float _colorLerpT;

    void Start()
    {
        _startEndWidth = endWidth;
        _startMaxDistance = maxDistance;
        _transform = transform;
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshFilter = GetComponent<MeshFilter>();
        _meshFilter.mesh = new Mesh();
    }

    private void Update()
    {
        _transform.rotation = Quaternion.identity;
    }

    private void FixedUpdate()
    {
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
            //Debug.DrawRay(origin[i], dir[i]);
            if (0 < Physics2D.Raycast(_origin[i], _dir[i], _contactFilter, hits, maxDistance))
            {
                _endRayPoint.Add(hits[0].point - _origin[i]);
                for (int j = 0; j < hits.Count; j++)
                {
                    if (hits[j].collider == _playerCollider)
                    {
                        playerVisible = true;
                        break;
                    }
                }
            }
            else
            {
                _endRayPoint.Add(_dir[i] * maxDistance);
            }
        }
        _vertices.Add(_rayStart.localPosition - _rayStart.up * _startWidth / 2);
        _vertices.Add(_endRayPoint[0]);
        for (int i = 1; i < _endRayPoint.Count; i++)
        {
            _vertices.Add(_endRayPoint[i]);
            //vertices.Add(endRayPoint[i + 1]);
            _triangles.Add(0);
            _triangles.Add(_vertices.Count - 1);
            _triangles.Add(_vertices.Count - 2);

        }

        _vertices.Add(_rayStart.localPosition + _rayStart.up * _startWidth / 2);
        //vertices.Add(endRayPoint[endRayPoint.Count - 1]);
        _triangles.Add(_endRayPoint.Count);
        _triangles.Add(0);
        _triangles.Add(_endRayPoint.Count + 1);

        _UVs.Add(Vector2.up);

        for (int i = 0; i < _endRayPoint.Count; i++)
        {
            float yUV = maxDistance / Vector2.SqrMagnitude(_endRayPoint[i]);
            _UVs.Add(new Vector2((float)i / _endRayPoint.Count, yUV));
        }
        _UVs.Add(Vector2.one);
        _meshFilter.mesh.vertices = _vertices.ToArray();
        _meshFilter.mesh.uv = _UVs.ToArray();
        _meshFilter.mesh.triangles = _triangles.ToArray();
        _meshFilter.mesh.RecalculateBounds();

        if (playerVisible) 
        {
            _colorLerpT = math.clamp(_colorLerpT + Time.fixedDeltaTime / _colorLerpTime, 0, 1);
        }
        else
        {
            _colorLerpT = math.clamp(_colorLerpT - Time.fixedDeltaTime / _colorLerpTime, 0, 1);
        }
        _meshRenderer.material.SetColor("_Color",Color.Lerp(_defaultColor, _playerVisibleColor, _colorLerpT));
    }

    public void ResetDistance()
    {
        maxDistance = _startMaxDistance;
    }

    public void ResetWidth()
    {
        endWidth = _startEndWidth;
    }

}
