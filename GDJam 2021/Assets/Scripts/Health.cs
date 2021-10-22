using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;
using UnityEngine.Events;

public class Health : MonoBehaviour
{

    [SerializeField] protected float health;

    public UnityEvent OnTakeDamage, OnDie;
    private bool deathDone = false;

    protected float _maxHP = 0.0f;

    [Header("Reference")]
    [SerializeField] new Rigidbody rigidbody = null;

    private void Start()
    {
        _maxHP = health;
        OnDie.AddListener(Die);
    }

    public float GetHealth()
    {
        return health;
    }
    public virtual void Die()
    {
    }
    public virtual void TakeDamage(float damage, Vector3 knockback)
    {
        OnTakeDamage.Invoke();
        rigidbody.AddForce(knockback, ForceMode.Impulse);
        health -= damage;
        if (health <= 0.0f)
            OnDie.Invoke();
    }

}
