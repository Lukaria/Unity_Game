using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonSword : Weapon
{
    [SerializeField] private float _selfDamage = 0.2f;
    [SerializeField] private float _heal = 1;
    private PlayerHealth _playerHealth;
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if(_playerHealth && _playerHealth.GetHealth() > 0)
            _playerHealth.TakeDamageWithoutAnim(_selfDamage);
    }

    private void Vampirick(GameObject obj)
    {
        _playerHealth.AddHealth(_heal);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        _playerAttack.OnAttack += Vampirick;
        _playerHealth = _playerAttack.gameObject.GetComponent<PlayerHealth>();

        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource)
        {
            audioSource.Play();
        }
    }
    
    public override void OnDisable()
    {
        base.OnDisable();
        _playerAttack.OnAttack -= Vampirick;
        _playerHealth = null;
    }

    private void OnDestroy()
    {
        _playerAttack.OnAttack -= Vampirick;
    }
}
