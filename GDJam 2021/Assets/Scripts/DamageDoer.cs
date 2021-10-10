﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageDoer : MonoBehaviour
{
    [SerializeField] float damage = 1.0f;
    public UnityEvent OnDidDamage;
    public bool canDoDamage = true;

    private void OnCollisionEnter(Collision other)
    {

        var hp = other.gameObject.GetComponent<Health>();
        if (hp == null)
            return;
        hp.TakeDamage(damage);
        OnDidDamage.Invoke();
    }
}