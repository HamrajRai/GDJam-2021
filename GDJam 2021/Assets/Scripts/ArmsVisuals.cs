using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsVisuals : MonoBehaviour
{
    [SerializeField] AnimationCurve lerpCurve = null;
    [SerializeField] Transform lerpPoint = null;
    [SerializeField] float lerpSpeed = 1.0f;

    Vector3 _originalPos = Vector3.zero;
    bool _enableLerping = true;
    private void OnEnable()
    {
        _originalPos = transform.localPosition;
        IEnumerator Lerp()
        {
            float x = 0.0f;
            while (true)
            {
                while (x < 1.0f && _enableLerping)
                {
                    yield return new WaitForEndOfFrame();
                    x += Time.deltaTime * lerpSpeed;
                    transform.localPosition = Vector3.Lerp(_originalPos, lerpPoint.localPosition, lerpCurve.Evaluate(x));
                }
                x = 1.0f;
                while (x > 0.0f && _enableLerping)
                {
                    yield return new WaitForEndOfFrame();
                    x -= Time.deltaTime * lerpSpeed;
                    transform.localPosition = Vector3.Lerp(_originalPos, lerpPoint.localPosition, lerpCurve.Evaluate(x));
                }
                x = 0.0f;
                while (!_enableLerping)
                    transform.localPosition = _originalPos;
            }
        }
        StartCoroutine(Lerp());
    }
}
