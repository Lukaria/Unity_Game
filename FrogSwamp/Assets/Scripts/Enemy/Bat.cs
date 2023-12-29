using Unity.VisualScripting;
using Random = System.Random;
using UnityEngine;

public class Bat : EnemyController
{

    [SerializeField] private float _attack01Damage = 1f;
    [SerializeField] private float _attack02Damage = 2f;

    public override void Update()
    {
        base.Update();
        
        if (isClosedForAttack())
        {
            if (_attackChance < 0.5f)
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
    
    public void RangedAttack02()
    {
        _attackChance = _rnd.NextDouble();

        var size = Physics.BoxCastNonAlloc(HitPoint.position, new Vector3(0.3f, 0.3f, 0.3f),
            HitPoint.forward, hits, HitPoint.rotation, 0f, playerLayerMask);

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
