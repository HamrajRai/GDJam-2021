using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningGun : Weapon
{
    [SerializeField] AnimationCurve lerpCurve = null;
    [SerializeField] float lerpSpeed = 1.0f;
    [SerializeField] float postShootTime = 0.5f;
    [SerializeField] float activateAt = 0.25f;

    [Header("References")]
    [SerializeField] Transform arm = null;
    [SerializeField] DamageDoer hitbox = null;
    [SerializeField] Transform lerpPoint = null;
    [SerializeField] WeaponManager weaponManager = null;


    Vector3 _originalPos = Vector3.zero;
    Quaternion _originalRot = Quaternion.identity;
    private void Awake()
    {
        _originalPos = arm.localPosition;
        _originalRot = arm.localRotation;
    }

    private void OnEnable()
    {
        arm.localPosition = _originalPos;
        arm.localRotation = _originalRot;
    }

    public override void Attack()
    {
        if (attacking)
            return;
        base.Attack();

        IEnumerator Lerp()
        {
            hitbox.canDoDamage = true;
            attacking = true;
            float x = 0.0f;
            while (x < 1.0f)
            {
                yield return new WaitForEndOfFrame();
                x += Time.deltaTime;
                arm.localPosition = Vector3.Lerp(_originalPos, lerpPoint.localPosition, lerpCurve.Evaluate(x));
                arm.localRotation = Quaternion.Slerp(_originalRot, lerpPoint.localRotation, lerpCurve.Evaluate(x));
                if (x >= activateAt)
                {
                    hitbox.gameObject.SetActive(true);
                }
                if (x >= activateAt + postShootTime)
                    hitbox.canDoDamage = false;

            }
            hitbox.gameObject.SetActive(false);
            yield return new WaitForSeconds(postShootTime);
            attacking = false;
            ammo = 1;
            weaponManager.ChangeWeapon(0);
        }
        StartCoroutine(Lerp());
    }
}
