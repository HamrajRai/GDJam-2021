using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fists : Weapon
{
    [System.Serializable]
    class ComboPart
    {
        public GameObject fist;
        [HideInInspector] public Vector3 orignalPos = Vector3.zero;
        public Transform lerpPos;
        public AnimationCurve punchCurve;
        public float comboSpeed = 1.0f;
    }

    [SerializeField] GameObject leftFist = null, rightFist = null;
    [SerializeField] List<ComboPart> combo = new List<ComboPart>();
    [SerializeField] float globalSpeed = 1.0f;
    [SerializeField] float comboCooldown = 0.25f;
    [Header("References")]
    [SerializeField] WeaponManager weaponManager = null;


    int _comboIndex = 0;
    bool _onCooldown = false;
    bool _attacking = false;

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
            f.orignalPos = f.fist.transform.localPosition;
    }

    public override void Attack()
    {
        if (_onCooldown || _attacking)
            return;
        base.Attack();
        IEnumerator Lerp()
        {
            _attacking = true;
            float x = 0.0f;
            float speed = globalSpeed + combo[_comboIndex].comboSpeed;
            while (x < 1.0f)
            {
                yield return new WaitForEndOfFrame();
                x += Time.deltaTime * speed;
                combo[_comboIndex].fist.transform.localPosition = Vector3.Lerp(combo[_comboIndex].orignalPos, combo[_comboIndex].lerpPos.localPosition, combo[_comboIndex].punchCurve.Evaluate(x));
            }
            _attacking = false;
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
