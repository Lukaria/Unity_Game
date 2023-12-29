using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float _damage = 1;
    
    [Inject] protected PlayerMovementScript playerMovementScript;

    protected PlayerAttack _playerAttack;

    virtual public void Awake()
    {
        _playerAttack = playerMovementScript.gameObject.GetComponent<PlayerAttack>();
    }

    virtual public void OnEnable()
    {
        Debug.Log(gameObject.name + " weapon was activated");
        Debug.Log(_damage);
        _playerAttack.SetAttackDamage(_damage);
    }

    virtual public void OnDisable()
    {
        //Debug.Log(gameObject.name + " weapon was deactivated");
    }

    public float GetDamage()
    {
        return _damage;
    }
}
