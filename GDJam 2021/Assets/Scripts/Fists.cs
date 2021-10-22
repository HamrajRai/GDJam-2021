using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fists : Weapon
{
    [System.Serializable]
    class ComboPart
    {
        public GameObject fist = null;
        [HideInInspector] public Vector3 orignalPos = Vector3.zero;
        [HideInInspector] public Quaternion orignalRotation = Quaternion.identity;
        public Transform lerpPos = null;
        public AnimationCurve punchCurve = null;
        [HideInInspector] public DamageDoer damageDoer = null;
        public float comboSpeed = 1.0f;
        public float damage = 1.0f;
        public float knockback = 5.0f;
    }

    [SerializeField] GameObject leftFist = null, rightFist = null;
    [SerializeField] List<ComboPart> combo = new List<ComboPart>();
    [SerializeField] float globalSpeed = 1.0f;
    [SerializeField] float comboCooldown = 0.25f;
    [Header("References")]
    [SerializeField] WeaponManager weaponManager = null;
    [SerializeField] new Rigidbody rigidbody = null;


    int _comboIndex = 0;
    bool _onCooldown = false;

    private void Start()
    {
        void OnFinishAttack()
        {
            _comboIndex = 0;
        }
        weaponManager.OnFinishAttack.AddListener(OnFinishAttack);

    }
    private void OnEnable()
    {
        foreach (var f in combo)
        {
            f.orignalPos = f.fist.transform.localPosition;
            f.orignalRotation = f.fist.transform.localRotation;
            f.damageDoer = f.fist.GetComponent<DamageDoer>();
            f.damageDoer.canDoDamage = false;
        }
        _onCooldown = false;
        attacking = false;

    }

    public override void Attack()
    {
        if (_onCooldown || attacking)
            return;
        base.Attack();
        IEnumerator Lerp()
        {
            combo[_comboIndex].damageDoer.damage = combo[_comboIndex].damage;
            combo[_comboIndex].damageDoer.knockBack = combo[_comboIndex].knockback;
            combo[_comboIndex].damageDoer.canDoDamage = true;
            attacking = true;
            float x = 0.0f;
            float speed = globalSpeed + combo[_comboIndex].comboSpeed;
            while (x < 1.0f)
            {
                yield return new WaitForEndOfFrame();
                x += Time.deltaTime * speed;
                combo[_comboIndex].fist.transform.localPosition = Vector3.Lerp(combo[_comboIndex].orignalPos, combo[_comboIndex].lerpPos.localPosition, combo[_comboIndex].punchCurve.Evaluate(x));
                combo[_comboIndex].fist.transform.localRotation = Quaternion.Slerp(combo[_comboIndex].orignalRotation, combo[_comboIndex].lerpPos.localRotation, combo[_comboIndex].punchCurve.Evaluate(x));
            }
            attacking = false;
            combo[_comboIndex].damageDoer.canDoDamage = false;
            _comboIndex++;
            if (_comboIndex == combo.Count)
            {
                _comboIndex = 0;
                _onCooldown = true;
                yield return new WaitForSeconds(comboCooldown);
                _onCooldown = false;
            }

        }
        StartCoroutine(Lerp());
    }
}
