using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using System;
using Random = UnityEngine.Random;

public class Lich : EnemyController
{
    [SerializeField] private float _collisionDamage = 1f;

    [SerializeField] private List<GameObject> _rocks = new List<GameObject>();
    
    [SerializeField] private float _rocksDamage = 1f;
    
    [SerializeField] private List<GameObject> _enemies = new List<GameObject>();
    [SerializeField] private float _spawnRange = 3;

    [SerializeField] private GameObject _fireball;
    [SerializeField] private float _fireballSpeed;
    
    [SerializeField] private GameObject _iceFalling;

    private GameObject _spawnedSpell = null;

    [Inject] private DiContainer _diContainer;

    public event Action OnDeath;

    [SerializeField] private float _meleeAttackRadius = 2.2f;
    [SerializeField] private float _rangeAttackRadius = 8f;

    [SerializeField] private int _minEnemiesSpawn = 1;
    [SerializeField] private int _maxEnemiesSpawn = 3;

    private bool _canAttack;
    
    private float _meleeAttackTime;
    private float _spinAttackTime;
    private float _spellAttackTime;
    
    
    void Awake()
    {
        base.Awake();
        _agent.stoppingDistance = _rangeAttackRadius;
        _canAttack = true;
        SetAnimationsTime();
        
        foreach (var rock in _rocks)
        {
            RockCollider armScript = rock.GetComponent<RockCollider>();
            armScript.SetAttackDamage(_rocksDamage);
        }
    }

    public override void SetAnimationsTime()
    {
        AnimationClip[] clips = _animator.runtimeAnimatorController.animationClips;
        foreach(AnimationClip clip in clips)
        {
            switch(clip.name)
            {
                case "CastSpell":
                    _spellAttackTime= clip.length;
                    break;
                case "SpinAttack":
                    _spinAttackTime = clip.length;
                    break;
                case "MeleeAttack":
                    _meleeAttackTime = clip.length;
                    break;
            }
        }
    }
    
    public override void Update()
    {
        base.Update();

        if (_inAttackRadius && _canAttack)
        {
            if (_distance <= _meleeAttackRadius) 
            {
                ExecuteRandMeleeAttack();
            }
            else
            {
                ExecuteRandRangedAttack();
            }
        }
    }

    void ExecuteRandMeleeAttack()
    {
        if (_attackChance <= 0.25f)
        {
            MeleeAttack();
        }
        else if(0.25f<_attackChance && _attackChance <= 0.50f)
        {
            SpinAttack();
        }
        else if(0.70f <_attackChance && _attackChance <= 0.95f)
        {
            IceFallingSpellAttack();
        }
        else
        {
            SpawnAttack();
        }
    }

    void ExecuteRandRangedAttack()
    {
        if (_attackChance <= 0.60f)
        {
            FireballSpellAttack();
        }
        else if(0.60f <= _attackChance && _attackChance <= 0.90f)
        {
            IceFallingSpellAttack();
        }
        else 
        {
            SpawnAttack();
        }
    }

    void MeleeAttack()
    {
        _attackChance = _rnd.NextDouble();
        StartCoroutine(WaitBeforeAttack(_meleeAttackTime * 1.5f));
        healthScript.SetImmortalFor(_meleeAttackTime/2);
        _animator.SetTrigger("MeleeAttackTrigger");
    }
    
    void SpinAttack()
    {
        _attackChance = _rnd.NextDouble();
        StartCoroutine(WaitBeforeAttack(_spinAttackTime * 2f));
        _animator.SetTrigger("SpinAttackTrigger");
    }
    
    void FireballSpellAttack()
    {
        _attackChance = _rnd.NextDouble();

        StartCoroutine(WaitBeforeAttack(_spellAttackTime));
        _animator.SetTrigger("SpellAttackTrigger");
        
        Rigidbody fireballRB = 
            Instantiate(_fireball, HitPoint.position, Quaternion.identity).
                GetComponent<Rigidbody>();

        fireballRB.velocity = transform.forward * _fireballSpeed;
    }

    void IceFallingSpellAttack()
    {
        _attackChance = _rnd.NextDouble();

        StartCoroutine(WaitBeforeAttack(_spellAttackTime * 3f));
        healthScript.SetImmortalFor(_spellAttackTime/2);
        _animator.SetTrigger("SpellAttackTrigger");
        
        Instantiate(_iceFalling, _target.position, Quaternion.identity);
    }

    void SpawnAttack()
    {
        _attackChance = _rnd.NextDouble();
        StartCoroutine(WaitBeforeAttack(_spellAttackTime * 1.2f));
        StartCoroutine(DisableAgentForSeconds(_spellAttackTime));
        _animator.SetTrigger("SpellAttackTrigger");

        float enemiesRandomCount = Random.Range(_minEnemiesSpawn, _maxEnemiesSpawn);
        for (int i = 0; i < enemiesRandomCount; ++i)
        {
            GameObject enemy = _diContainer.InstantiatePrefab(
                _enemies[Random.Range(0, _enemies.Count)]);

            Vector2 randomRadius = Random.insideUnitCircle * _spawnRange;
            
            enemy.GetComponent<NavMeshAgent>().Warp(
                new Vector3(randomRadius.x, 0f ,randomRadius.y) + transform.position
                );
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();
        if (health)
        {
            health.TakeDamage(_collisionDamage);
        }
    }
    
    public override void TakeDamage()
    {
        _animator.SetTrigger("TakeDamageTrigger");
        healthScript.SetImmortalFor(0.5f);
    }

    public override void Death()
    {
        base.Death();
        GetComponent<BoxCollider>().enabled = false;
        
        foreach (var rock in _rocks)
        {
            RockCollider armScript = rock.GetComponent<RockCollider>();
            armScript.SetAttackDamage(0);
        }
        OnDeath?.Invoke();
    }

    IEnumerator WaitBeforeAttack(float waitTime)
    {
        _canAttack = false;
        yield return new WaitForSeconds(waitTime);
        _canAttack = true;
    }
    
    private void OnDrawGizmos()
    {
        var position = transform.position;
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, _spawnRange);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(position, _rangeAttackRadius);
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(position, _meleeAttackRadius);
        
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(position, _lookRadius);
    }
}
