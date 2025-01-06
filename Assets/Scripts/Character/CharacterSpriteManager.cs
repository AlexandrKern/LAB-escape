using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpriteManager : MonoBehaviour
{
    [SerializeField] private CharacterHealth _characterHealth;
    [SerializeField] private Sprite[] _sprites;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        _characterHealth.OnHealthChanged += ChangeSprite;
    }
    private void OnDisable()
    {
        _characterHealth.OnHealthChanged += ChangeSprite;
    }

    private void ChangeSprite(int healthCount)
    {
        float healthPercentage = ((float)healthCount / _characterHealth.MaxHealth) * 100f;

        if (healthPercentage >= 75)
        {
            _spriteRenderer.sprite = _sprites[0];
        }
        else if (healthPercentage < 75 && healthPercentage >= 50)
        {
            _spriteRenderer.sprite = _sprites[1];
        }
        else if (healthPercentage < 50 && healthPercentage >= 25)
        {
            _spriteRenderer.sprite = _sprites[2];
        }

    }
}
