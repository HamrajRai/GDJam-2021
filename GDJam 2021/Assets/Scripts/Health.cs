using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;
using UnityEngine.Events;

public class Health : MonoBehaviour
{

    [SerializeField] float health;

    public UnityEvent OnTakeDamage, OnDie;
    private bool deathDone = false;
    public void TakeDamage(float damage)
    {
        IEnumerator func()
        {
            void Dead() => deathDone = true;

            health -= damage;
            OnTakeDamage.Invoke();
            if (health <= 0)
            {
                OnDie.Invoke();

                yield return new WaitUntil(
                    () =>
                {
                    //this should be the last thing called in the listeners
                    OnDie.AddListener(Dead);
                    OnDie.Invoke();
                    return deathDone;//I finished my kill spree
                });

                OnDie.RemoveListener(Dead);
                gameObject.SetActive(false);
            }
        }
        StartCoroutine(func());
    }

}
