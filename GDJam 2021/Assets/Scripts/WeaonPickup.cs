using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaonPickup : MonoBehaviour
{
    [System.Serializable]
    public enum WeaponType
    {
        None,
        Fire,
        Thunder,
        Meteor,
        Falcon,
    }
    [SerializeField] AnimationCurve lerpCurve = null;
    [SerializeField] Transform lerpPoint = null;
    [SerializeField] float lerpSpeed = 1.0f;
    [SerializeField] WeaponType type = WeaponType.Fire;

    Vector3 _originalPos = Vector3.zero;
    // Start is called before the first frame update
    void OnEnable()
    {
        _originalPos = transform.localPosition;
        IEnumerator Lerp()
        {
            while (true)
            {
                float x = 0.0f;
                while (x < 1.0f)
                {
                    yield return new WaitForEndOfFrame();
                    x += Time.deltaTime * lerpSpeed;
                    transform.localPosition = Vector3.Lerp(_originalPos, lerpPoint.localPosition, lerpCurve.Evaluate(x));
                }
            }
        }
        StartCoroutine(Lerp());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        other.gameObject.GetComponentInChildren<WeaponManager>().ChangeWeapon(((int)type));
        Destroy(gameObject.transform.parent.gameObject);
    }
}
