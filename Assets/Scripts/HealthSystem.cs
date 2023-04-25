using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    int _health;
    [SerializeField]
    int _maxHealth;
    
    void OnEnable()
    {
        _health = _maxHealth;
    }

    void AddLive(int amount)
    {
        _health = _health + amount;
        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }
    }

    void GetDamage(int amount)
    {
        _health = _health - amount;
        if (_health <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        Destroy(gameObject);
    }
}
