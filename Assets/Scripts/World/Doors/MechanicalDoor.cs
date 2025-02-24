using UnityEngine;

public class MechanicalDoor : MonoBehaviour, IDamageable
{
    [SerializeField]
    private Sprite[] sprites;

    private SpriteRenderer _spriteRenderer;
    private int _currentSpriteIndex = 0;

    private Animator _animator;
    private bool _isOpen;
    private BoxCollider2D _boxCollider;

    private void Awake()
    {
        _isOpen = false;
        _boxCollider = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = sprites[0];
    }

    private void Start()
    {
        _animator.enabled = false; 
    }

    public void CauseDamage()
    {
        if(_isOpen) return;
        _currentSpriteIndex++;
        if (_currentSpriteIndex >= sprites.Length)
        {
            Destroy(gameObject);
            return;
        }
        _spriteRenderer.sprite = sprites[_currentSpriteIndex];
    }

    public void Open()
    {
        if(_currentSpriteIndex>0) return;
        if(Character.Instance.GetCharacterForm() == FormType.Base && !_isOpen)
        {
            _animator.enabled = true;
            _isOpen = true;
            _animator.Play("DoorOpenAnimation");
        }
    }

    public void OnBoxTrigger()
    {
        _boxCollider.isTrigger = true;
    }
}
