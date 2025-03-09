using System;
using UnityEngine;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;

public class CharacterHealth : MonoBehaviour
{
    SceneLoader loader = new SceneLoader();

    [SerializeField] private int _maxHealth = 400;
    [SerializeField] private GameObject bloodScreen;
    private int _currentHealth;

    public int MaxHealth => _maxHealth;
    public int CurrentHealth => _currentHealth;

    [HideInInspector] public Action<int> OnHealthChanged;
    [HideInInspector] public UnityEvent OnDeath;

    private Swarm swarm;

    private bool isDead = false;

  

    private void Start()
    {
        Data.FullHP = _maxHealth;
        swarm = GetComponent<Swarm>();
        if (Data.HP == 0)
        {
            Data.HP = _maxHealth;
            _currentHealth = Data.HP;
        }
        else
        {
            _currentHealth = Data.HP;
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        DamageEffect().Forget();

        _currentHealth -= damage;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        swarm.SetUnitCount(_currentHealth);
        OnHealthChanged?.Invoke(_currentHealth);
        Data.HP = _currentHealth;
        if (_currentHealth <= 0)
        {
            Die(); 
        }
    }

    private void Die()
    {
        isDead = true;
        loader.LoadSceneAsync("Biom1");
        OnDeath?.Invoke();
    }

    public void ResetHealth()
    {
        _currentHealth = _maxHealth;
        swarm.SetUnitCount(_currentHealth);
        Data.HP = _currentHealth;
        isDead = false;
    }

    async UniTask DamageEffect()
    {
        bloodScreen?.gameObject.SetActive(true);
        await UniTask.Delay(500);
        bloodScreen?.gameObject.SetActive(false);
    }
}
