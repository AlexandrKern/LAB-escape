using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column : MonoBehaviour, IDamageable
{
    [SerializeField]
    private Sprite[] sprites;

    private SpriteRenderer _spriteRenderer;
    private int _currentSpriteIndex = 0;

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _spriteRenderer.sprite = sprites[0];
    }

    public void CauseDamage()
    {
        _currentSpriteIndex++;
        if(_currentSpriteIndex >= sprites.Length)
        {
            Destroy(gameObject);
            return;
        }
        _spriteRenderer.sprite = sprites[_currentSpriteIndex];
    }
}
