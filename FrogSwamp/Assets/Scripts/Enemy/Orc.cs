using System;
using UnityEngine;

public class Orc : EnemyController
{
    [SerializeField] private float _Attack01Damage = 1f;
    [SerializeField] private float _Attack02Damage = 1f;

    public override void Update()
    {
        base.Update();

        if (isClosedForAttack())
        {

            if (_attackChance < 0.85)
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
        
        var size = Physics.BoxCastNonAlloc(HitPoint.position, new Vector3(0.515f, 0.515f, 0.515f),
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
        
        var size = Physics.BoxCastNonAlloc(HitPoint.position, new Vector3(0.515f, 0.515f, 0.515f),
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
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(HitPoint.position, 0.515f);
    }
}
