using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    [Tooltip("-1 for infinite")]
    [SerializeField] protected int ammo = -1;


    public UnityEvent OnAttack;

    public int GetAmmo()
    {
        return ammo;
    }

    public virtual void Attack()
    {
        if (ammo != -1)
            ammo--;
        OnAttack.Invoke();
    }



}
