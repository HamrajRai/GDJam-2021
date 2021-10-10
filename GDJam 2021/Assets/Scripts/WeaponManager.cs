using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] List<Weapon> weapons = new List<Weapon>();
    [SerializeField] float downtime = 0.25f;
    public UnityEvent OnChangeWeapon;
    public UnityEvent OnBeginAttack;
    public UnityEvent OnFinishAttack;

    int _weaponIndex = 0;
    Weapon _currentWeapon = null;
    bool _canBackstab = false;

    float _internalDowntime = 0.0f;

    private void Start()
    {
        _currentWeapon = weapons[0];
    }

    private void Update()
    {
        _internalDowntime += Time.deltaTime;
        if (_internalDowntime >= downtime)
            OnFinishAttack.Invoke();
    }

    public void AttackWithCurrentWeapon()
    {
        if (_currentWeapon.GetAmmo() == 0)
            return;
        _internalDowntime = 0.0f;
        OnBeginAttack.Invoke();
        _currentWeapon.Attack();
    }

    public void ChangeWeapon(int index)
    {
        _weaponIndex = index;
        _currentWeapon = weapons[_weaponIndex];
        OnChangeWeapon.Invoke();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Terrain") || other.gameObject.CompareTag("Unassigned"))
            return;
        _canBackstab = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Terrain") || other.gameObject.CompareTag("Unassigned"))
            return;
        _canBackstab = false;
    }
}
