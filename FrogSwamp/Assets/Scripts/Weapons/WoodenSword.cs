using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenSword : Weapon
{
    private float _currentDamage;
    
    public override void OnEnable()
    {
        base.OnEnable();
        _currentDamage = GetDamage();
        _playerAttack.OnAttack += UpDamage;
    }

    private void UpDamage(GameObject obj)
    {
        _currentDamage *= 2f;
        _playerAttack.SetAttackDamage(_currentDamage);
        Debug.Log(_currentDamage);
    }

    public override void OnDisable()
    {
        base.OnDisable();
        _playerAttack.OnAttack -= UpDamage;
    }
    
    void OnDestroy()
    {
        _playerAttack.OnAttack -= UpDamage;
    }
}
