using UnityEngine;

public partial class Swarm
{
    public class Unit
    {
        private Transform _transform;
        private SpriteRenderer _spriteRenderer;
        private DestinationPoint _destination;
        private float _t;
        private float _speed;
        private int _sortingOrder;

        public Unit(Transform transform, DestinationPoint destination, SpriteRenderer spriteRenderer)
        {
            _t = Random.Range(0, Mathf.PI * 2);
            this.Transform = transform;
            this.Destination = destination;
            _spriteRenderer = spriteRenderer;
        }

        public Transform Transform { get => _transform; set => _transform = value; }
        public DestinationPoint Destination { get => _destination; set => _destination = value; }

        public void SetDestenation(DestinationPoint destinationPoint)
        {
            Destination = destinationPoint;
        }

        public void UpdateUnit(Swarm swarm, bool Transation, bool visible)
        {
            _spriteRenderer.enabled = visible;
            if (Vector3.Distance(Transform.position, Destination.Transform.position) < 0.05f)
            {
                _spriteRenderer.sortingOrder = _sortingOrder;
                Destination.IsFree = true;
                var destenationPoint = swarm.GetRandomEmptyPoint(this);
                destenationPoint.IsFree = false;
                SetDestenation(destenationPoint);
                if (Transation)
                {
                    _speed = Random.Range(swarm.MinTranslationSpeed, swarm.MaxTranslationSpeed);
                }
                else
                {
                    _speed = Random.Range(swarm.MinSpeed, swarm.MaxSpeed);
                }
            }
            else
            {
                Vector3 direction = Vector3.Normalize(Destination.Transform.position - Transform.position);

                Transform.position = Transform.position += direction * Time.deltaTime * _speed;
                Transform.right = direction;
            }

            float size = (Mathf.Sin(_t * 2) * (swarm.MaxSize - swarm.MinSize) + swarm.MinSize * 2);
            _transform.localScale = new Vector3(size, size, size);
            _t += Time.deltaTime;

        }

        public void SetSortingOrder(int order)
        {
            _sortingOrder = order;
        }
    }
}

