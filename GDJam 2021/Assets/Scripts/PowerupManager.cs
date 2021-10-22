using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    [SerializeField] GameObject lightningPowerup = null;
    public void OnEnemyDie(Transform t)
    {
        lightningPowerup.SetActive(true);
        lightningPowerup.transform.position = t.position;
    }
}
