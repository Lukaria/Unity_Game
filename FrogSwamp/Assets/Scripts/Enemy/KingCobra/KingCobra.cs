using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using System.Collections;

public class KingCobra : EnemyController
{

    [SerializeField] private float _biteAttackDamage = 3.0f;

    [SerializeField] private GameObject _spitPoison;

    
    void Update()
    {
        base.Update();
        
        if (isClosedForAttack())
        {
            if (_attackChance < 0.15f)
            {
                _animator.SetTrigger("Attack02Trigger");
                StartCoroutine(DisableAgentForSeconds(_attack02Time));
            }
            else
            {
                _animator.SetTrigger("Attack01Trigger");
                StartCoroutine(DisableAgentForSeconds(_attack01Time));
            }
        }
    }
    
    public void BiteMeleeAttack()
    {
        _attackChance = _rnd.NextDouble();
        
        var size = Physics.BoxCastNonAlloc(HitPoint.position, new Vector3(0.5f, 0.5f, 0.5f),
            HitPoint.forward, hits, HitPoint.rotation, 0f, playerLayerMask);

        if (size>0)
        {
            PlayerHealth playerHealth = hits[0].transform.gameObject.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(_biteAttackDamage);
        }
    }
    
    public void SpitPoisonAttack()
    {
        _attackChance = _rnd.NextDouble();

        Instantiate(_spitPoison, gameObject.transform.position + transform.forward * 2f, Quaternion.identity);
    }

    public override void Death()
    {
        killCounter.EnemyKilledSignal();
        base.Death();
    }
}
