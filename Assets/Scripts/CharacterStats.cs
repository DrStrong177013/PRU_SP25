using System;
using System.Security.Cryptography;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    public Stat strength;
    public Stat damage;
    public Stat maxHealth;
    [SerializeField] private int currentHealth;

    protected virtual void Start()
    {
        currentHealth = maxHealth.GetValue();

        //example equip sword with 4 damage
        //damage.AddModifiers(4);
    }
    public virtual void DoDamage(CharacterStats _targetStats)
    {
        int totalDamage = damage.GetValue() + strength.GetValue();
        _targetStats.TakeDamage(totalDamage);
    }

    public virtual void TakeDamage(int _damage)
    {
        currentHealth -= _damage;
        Debug.Log(_damage);
        if (currentHealth < 0)
            Die();
    }

    protected virtual void Die()
    {
        throw new NotImplementedException();
    }

}
