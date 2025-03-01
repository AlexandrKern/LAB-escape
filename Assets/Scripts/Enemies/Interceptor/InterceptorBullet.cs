using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterceptorBullet : MonoBehaviour
{
    public int damage = 1;
    private CharacterHealth _health;

    private void Start()
    {
        _health = Character.Instance.GetComponent<CharacterHealth>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _health.TakeDamage(damage);
            Debug.Log("нанес урон. осталось " + _health.CurrentHealth);
        }
    }
}
