using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using Random = System.Random;

public class AxeOrc : EnemyController
{

    [SerializeField] private float _attack01Damage = 3.0f;
    [SerializeField] private float _attack02Damage = 3.0f;

    [SerializeField] private Transform HitPoint02;


    public override void Update()
    {
        base.Update();
        
        if (isClosedForAttack())
        {
            if (_attackChance < 0.7f)
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
        
        var size = Physics.BoxCastNonAlloc(HitPoint.position, new Vector3(0.3f, 0.3f, 0.3f),
            HitPoint.forward, hits, HitPoint.rotation, 0f, playerLayerMask);

        if (size>0)
        {
            PlayerHealth playerHealth = hits[0].transform.gameObject.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(_attack01Damage);
        }
    }
    public void MeleeAttack02()
    {
        _attackChance = _rnd.NextDouble();
        
        var size = Physics.BoxCastNonAlloc(HitPoint02.position, new Vector3(0.3f, 0.3f, 0.3f),
            HitPoint.forward, hits, HitPoint02.rotation, 0f, playerLayerMask);
        
        if (size>0)
        {
            PlayerHealth playerHealth = hits[0].transform.gameObject.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(_attack02Damage);
        }
    }
    

    public override void Death()
    {
        killCounter.EnemyKilledSignal();
        base.Death();
    }
}
