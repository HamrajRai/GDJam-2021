using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageDoer : MonoBehaviour
{
    public float damage = 1.0f;
    public float knockBack = 5.0f;
    public UnityEvent OnDidDamage;
    public bool canDoDamage = true;


    private void OnTriggerEnter(Collider other)
    {
        if (!canDoDamage || other.CompareTag(gameObject.tag))
            return;
        var hp = other.gameObject.GetComponent<Health>();
        if (hp == null)
            return;
        hp.TakeDamage(damage, (other.transform.position - transform.position).normalized * knockBack);
        OnDidDamage.Invoke();
    }
}
