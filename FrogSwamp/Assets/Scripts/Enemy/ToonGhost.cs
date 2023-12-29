using UnityEngine;

public class ToonGhost : EnemyController
{
    [SerializeField] private float _biteAttackDamage = 1f;
    
    [SerializeField] private float _spellAttackDamage = 3f;

    public override void Update()
    {
        base.Update();

        if (isClosedForAttack())
        {
            if (_attackChance < 0.6)
            {
                _animator.SetTrigger("Attack01Trigger");
            }
            else
            {
                _animator.SetTrigger("Attack02Trigger");
            }
        }
    }

    public void BiteMeleeAttack()
    {
        _attackChance = _rnd.NextDouble();
        
        var size = Physics.BoxCastNonAlloc(HitPoint.position, new Vector3(0.05f, 0.05f, 0.15f),
            HitPoint.forward, hits, HitPoint.rotation, 0f, playerLayerMask);
        
        if (size>0)
        {
            PlayerHealth playerHealth = hits[0].transform.gameObject.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(_biteAttackDamage);
        }
    }

    public void SpellMeleeAttack()
    {
        _attackChance = _rnd.NextDouble();
        
        var size = Physics.BoxCastNonAlloc(HitPoint.position, new Vector3(0.05f, 0.05f, 0.15f),
            HitPoint.forward, hits, HitPoint.rotation, 0f, playerLayerMask);
        
        if (size>0)
        {
            
            PlayerHealth playerHealth = hits[0].transform.gameObject.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(_spellAttackDamage);
        }
    }

    public override void Death()
    {
        killCounter.EnemyKilledSignal();
        GetComponent<BoxCollider>().enabled = false;
        base.Death();
    }
 
}
