using UnityEngine;

public class Wolf : EnemyController
{

    [SerializeField] private float _biteAttackDamage = 1f;
    
    public override void Update()
    {
        base.Update();

        if (_inAttackRadius)
        {
            _animator.SetTrigger("Attack01Trigger");
            StartCoroutine(DisableAgentForSeconds(_attack01Time));
        }
    }

    public void BiteMeleeAttack()
    {
        
        _attackChance = _rnd.NextDouble();
        
        var size = Physics.BoxCastNonAlloc(HitPoint.position, new Vector3(0.3f, 0.3f, 0.3f),
            HitPoint.forward, hits, HitPoint.rotation, 0f, playerLayerMask);
        
        if (size>0)
        {
            PlayerHealth playerHealth = hits[0].transform.gameObject.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(_biteAttackDamage);
        }
    }
    
    public override void Death()
    {
        killCounter.EnemyKilledSignal();
        base.Death();
    }
}


