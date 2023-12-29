using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class Skeleton : EnemyController
{
    [SerializeField] private float _Attack01Damage = 1f;
    [SerializeField] private float _Attack02Damage = 1f;

    public override void Update()
    {
        base.Update();
        
        if (isClosedForAttack())
        {
            if (_attackChance < 0.6)
            {
                _animator.SetTrigger("Attack01Trigger");
                StartCoroutine(DisableAgentForSeconds(_attack01Time));
            }
            else
            {
                _animator.SetTrigger("Attack02Trigger");
                StartCoroutine(DisableAgentForSeconds(_attack02Time));
            }
        }
    }

    public void MeleeAttack01()
    {
        _attackChance = _rnd.NextDouble();
        
        var size = Physics.BoxCastNonAlloc(HitPoint.position, new Vector3(0.08f, 0.08f, 0.08f),
            HitPoint.forward, hits, HitPoint.rotation, 0f, playerLayerMask);
        
        if (size>0)
        {
            PlayerHealth playerHealth = hits[0].transform.gameObject.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(_Attack01Damage);
        }
    }

    public void MeleeAttack02()
    {
        _attackChance = _rnd.NextDouble();
        
        var size = Physics.BoxCastNonAlloc(HitPoint.position, new Vector3(0.08f, 0.08f, 0.8f),
            HitPoint.forward, hits, HitPoint.rotation, 0f, playerLayerMask);
        
        if (size>0)
        {
            PlayerHealth playerHealth = hits[0].transform.gameObject.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(_Attack02Damage);
        }
    }
    
    public override void Death()
    {
        killCounter.EnemyKilledSignal();
        base.Death();
    }
}
