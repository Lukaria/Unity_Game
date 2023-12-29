using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmCollider : MonoBehaviour
{
    private float _attackDamage = 3f;
    void OnTriggerEnter(Collider collider)
    {
        PlayerHealth health = collider.gameObject.GetComponent<PlayerHealth>();
        if (health)
        { 
            health.TakeDamage(_attackDamage);
        }
    }

    public void SetAttackDamage(float value)
    {
        _attackDamage = value;
    }
}
