using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HolyLight : Ability
{
    [SerializeField] private HolyLightStats _holyLightStats;

    private List<GameObject> array;

    private void Awake()
    {
        array = new List<GameObject>();
        ConvertContext(_holyLightStats);
    }

    private GameObject[] enemies;
    
    public override void Activate(GameObject go)
    {
        base.Activate(go);
        
        StartCoroutine(HolyLightDOT());
    }

    bool isInRadius(Vector3 pos, float radius)
    {
        return Vector3.Distance(pos, transform.position) < radius; 
    }
    IEnumerator HolyLightDOT()
    {
        for (int i = 0; i < _holyLightStats.TickCount; ++i)
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var enemy in enemies)
            {
                Health healthScript = enemy.GetComponent<Health>();
                if (healthScript != null && 
                    healthScript.GetCurrentHealth() > 0 && 
                    isInRadius(healthScript.gameObject.transform.position, _holyLightStats.AbilityRadius))
                {
                    healthScript.TakeDamage( _holyLightStats.Damage);

                    array.Add(
                        Instantiate(_holyLightStats.Effect,
                        healthScript.gameObject.transform.position,
                        Quaternion.identity)
                    );


                }
            }
            yield return new WaitForSeconds( _holyLightStats.WaitTime);
        }
    }

    public override void Cooldown()
    {
        base.Cooldown();
        foreach (var effect in array)
        {
            Destroy(effect);
        }
        array.Clear();
    }
}
