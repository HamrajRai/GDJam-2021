using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballGun : Weapon
{
    [SerializeField] Transform lerpPos;

    Vector3 _orignalPos = Vector3.zero;
    public AnimationCurve throwCurve;

    private void Start()
    {
        _orignalPos = transform.localPosition;
    }

    ///<summary>Animation that played after gun fired</summary>
    IEnumerator DoFireAnimation()
    {
        Animator thing =
        !GetComponent<Animator>() ?
        new Animator() : GetComponent<Animator>();


        IEnumerator Lerp()
        {
            float x = 0.0f;
            while (x < 1.0f)
            {
                yield return new WaitForEndOfFrame();
                x += Time.deltaTime;
                Mathf.Lerp(0.0f, 1.0f, throwCurve.Evaluate(x));
            }
        }
        yield return Lerp();
        gameObject.SetActive(false);
    }

    void createClip()
    {
        AnimationClip clip = new AnimationClip();

        var ev = new AnimationEvent();
        ev.time = .01f;
        ev.functionName = "randomPosRot";
        //clip.AddEvent();
    }

    void randomPosRot()
    {

    }

    public override void Attack()
    {
        StartCoroutine(DoFireAnimation());

        base.Attack();
    }

}
