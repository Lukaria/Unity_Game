using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;


[RequireComponent(typeof(Animator))]
public class Health : MonoBehaviour
{
    #region Health & Immortality

    [SerializeField] private float _immortalityTime = 0f;
    private float _immortalityCurrentTime;
    private bool _isImmortal;

    [SerializeField] private float _maxHealth;
    [SerializeField] private float _regeneration = 0f;
    private float _currentHealth;

    #endregion

    private Animator animator;

    private HealthBarScript healthBarScriptUI;

    #region Getters

    public float GetCurrentHealth()
    {
        return _currentHealth;
    }

    #endregion

    public event Action onTakeDamage; 
    public event Action onDie;

    private bool _isDead;

    void Awake()
    {
        _isDead = false;
        _isImmortal = false;
        _immortalityCurrentTime = _immortalityTime;
        _currentHealth = _maxHealth;

        animator = GetComponent<Animator>();
        
        healthBarScriptUI = GetComponentInChildren<HealthBarScript>();
        
        healthBarScriptUI.SetMaxhealth(_maxHealth);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(_currentHealth > 0 && _currentHealth < _maxHealth)
            _currentHealth += _regeneration * Time.deltaTime;//passive regeneration
        
        if (_isImmortal)
        {
            _immortalityCurrentTime -= Time.deltaTime;
        }
        
        if (_immortalityCurrentTime <= 0)
        {
            _immortalityCurrentTime = _immortalityTime;
            _isImmortal = false;
        }
        
        
        UpdateHealthBar(_currentHealth);
    }

    
    public virtual void TakeDamage(float damage)
    {
       if(!_isImmortal)
       {
           _currentHealth -= damage;
       }
       
       if (_currentHealth > 0)
       {
           if(!_isImmortal)
               onTakeDamage?.Invoke();
       }
       else if(! _isDead)
       {
           Death();
       }
    }
    
    public void TakeDamageWithoutAnim(float damage)
    {
        if(!_isImmortal)
        {
            _currentHealth -= damage;
        }
        if (_currentHealth < 0 && !_isDead){
            Death();
        }
    }

    public virtual void Death()
    {
        _isDead = true;
        onDie?.Invoke();
        healthBarScriptUI.DisableHealthBar();
    }

    public virtual void UpdateHealthBar(float health)
    {
        if (healthBarScriptUI != null)
        {
            healthBarScriptUI.SetHealth(health);
        }
    }
    
    public void AddMaxHealth(float health)
    {
        _maxHealth += health;
        _currentHealth += health;
    }

    public void SubstractMaxHealth(float health)
    {
        _maxHealth -= health;
        _currentHealth -= health;
    }
    
    public void AddHealth(float health)
    {
        _currentHealth += health;
        if (_currentHealth > _maxHealth) _currentHealth = _maxHealth;
    }

    public void SetImmortalFor(float value)
    {
        _immortalityTime = value;
        _immortalityCurrentTime = _immortalityTime;
        _isImmortal = true;
    }
    
}
