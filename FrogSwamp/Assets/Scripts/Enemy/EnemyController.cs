using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Random = System.Random;

[RequireComponent(typeof(Health))]
public class EnemyController : MonoBehaviour
{
    #region Stats

    [SerializeField] protected float _maxSpeed;
    protected float _currentSpeed;

    [SerializeField] protected float _defaultAttackDamage;
    protected float _currentAttackDamage;

    [SerializeField] protected float _maxStun;
    protected float _currentStun;
    
    [SerializeField] protected float _lookRadius = 10f;

    #endregion

    #region BoxCast for Bite Attack

    [SerializeField] protected Transform HitPoint;
    [SerializeField] protected LayerMask playerLayerMask;
    protected RaycastHit[] hits = new RaycastHit[3];

    #endregion
    
    [Inject] protected PlayerMovementScript playerMovementScript;

    [Inject] protected KillCounter killCounter; 
    
    [SerializeField] protected Transform _target;
    
    protected NavMeshAgent _agent;

    protected float _distance;

    protected bool _inAttackRadius = false;

    protected Health healthScript;

    public event Action<GameObject> effectsHolder; 
    
    
    protected Animator _animator;
    
    protected Random _rnd;
    protected double _attackChance;

    #region Animations time

    protected float _attack01Time = 0f;
    protected float _attack02Time = 0f;
    protected float _tauntTime = 0f;
    
    #endregion
    
    #region Sounds
    protected AudioSource _deathSource;
    [SerializeField] private AudioClip _deathSound;
    
    #endregion

    private bool _canTaunt = true;
    

    public bool isClosedForAttack()
    {
        return _inAttackRadius;
    }

    public virtual void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();

        healthScript = GetComponent<Health>();

        _currentSpeed = _maxSpeed;
        _currentAttackDamage = _defaultAttackDamage;
        _currentStun = 0f;
        
        _agent.speed = _currentSpeed;
        
        _target = playerMovementScript.transform;

        _rnd = new Random();
        
                
        _animator = GetComponent<Animator>();
 
        healthScript.onTakeDamage += TakeDamage;
        healthScript.onDie += Death;

        _attackChance = _rnd.NextDouble();
        
        SetAnimationsTime();
        SetSounds();
    }

    public virtual void SetSounds()
    {
        if (_deathSound)
        {
            _deathSource = gameObject.AddComponent<AudioSource>();
            _deathSource.loop = false;
            _deathSource.playOnAwake = false;
            _deathSource.spatialBlend = 1f;
            _deathSource.enabled = true;
            _deathSource.clip = _deathSound;
        }
    }
    
    public virtual void SetAnimationsTime()
    {
        AnimationClip[] clips = _animator.runtimeAnimatorController.animationClips;
        foreach(AnimationClip clip in clips)
        {
            switch(clip.name)
            {
                case "Attack01":
                    _attack01Time = clip.length;
                    break;
                case "Attack02":
                    _attack02Time = clip.length;
                    break;
                case "Taunting":
                    _tauntTime = clip.length;
                    break;
            }
        }
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (healthScript.GetCurrentHealth() <= 0 || ! _agent.enabled)
        {
            _inAttackRadius = false;
            return;
        }
        
        _animator.SetFloat("RunForward", 0.0f);

        _distance = Vector3.Distance(_target.position, transform.position);

        if (_distance <= _lookRadius)
        {
            if (_canTaunt && _tauntTime != 0f)
            {
                _animator.SetTrigger("TauntTrigger");
                StartCoroutine(DisableAgentForSeconds(_tauntTime));
                FaceTarget();
                _canTaunt = false;
                return;
            }
            
            _animator.SetFloat("RunForward", 5.0f);

            _agent.SetDestination(_target.position);

            if (_distance <= _agent.stoppingDistance)
            {
                _animator.SetFloat("RunForward", 0.0f);

                FaceTarget();
                _inAttackRadius = true;
            }
            else
            {
                _inAttackRadius = false;
            }
        }
        else
        {
            _canTaunt = true;
        }
    }

    public virtual void FixedUpdate()
    {
        effectsHolder?.Invoke(gameObject);
    }

    protected void FaceTarget()
    {
        Vector3 direction = (_target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 40);
    }
    
    public virtual void TakeDamage()
    {
        _animator.SetTrigger("TakeDamageTrigger");
    }

    public virtual void Death()
    {
        
        _agent.enabled = false;
        _animator.SetTrigger("DieTrigger");

        if(_deathSource) _deathSource.Play();
    }

    public IEnumerator DisableAgentForSeconds(float time)
    {
        _agent.enabled = false;
        yield return new WaitForSeconds(time);
        _agent.enabled = true;
    }

    public void SetCurrentSpeed(float value)
    {
        _currentSpeed = value;
        _agent.speed = _currentSpeed;
    }

    public float GetCurrentSpeed()
    {
        return _currentSpeed;
    }

    public void RestoreSpeed()
    {
        _currentSpeed = _maxSpeed;
        _agent.speed = _maxSpeed;
    }
}
