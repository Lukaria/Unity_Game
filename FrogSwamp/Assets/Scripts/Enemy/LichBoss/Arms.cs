using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Arms : EnemyController
{
    [SerializeField] private float _attackDamage = 3f;
    [FormerlySerializedAs("armColliders")] [SerializeField] private List<GameObject> _arms = new List<GameObject>();
    [SerializeField] private float _delay = 2f;
    [FormerlySerializedAs("_attackRadius")] [SerializeField] private float _attackRange = 1f;

    private bool _canAttack = true;

    public override void Awake()
    {
        base.Awake();
        _agent = null;
        foreach (var arm in _arms)
        {
            ArmCollider armScript = arm.GetComponent<ArmCollider>();
            armScript.SetAttackDamage(_attackDamage);
        }
    }

    public override void Update()
    {
        if (healthScript.GetCurrentHealth() <= 0)
        {
            _inAttackRadius = false;
            return;
        }
        
        _distance = Vector3.Distance(_target.position, transform.position);

        if (_distance <= _attackRange && _canAttack)
        {
            _animator.SetTrigger("AttackTrigger");
            StartCoroutine(AttackDelay(_delay));
        }
    }
    
    public override void Death()
    {
        _animator.SetTrigger("DieTrigger");
        
        foreach (var arm in _arms)
        {
            ArmCollider armScript = arm.GetComponent<ArmCollider>();
            armScript.SetAttackDamage(0);
        }
    }

    IEnumerator AttackDelay(float delay)
    {
        _canAttack = false;
        yield return new WaitForSeconds(delay);
        _canAttack = true;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }
}
