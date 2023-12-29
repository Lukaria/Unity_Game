using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityHolder : MonoBehaviour
{
    IAbility _ability;

    [SerializeField] InputActionReference abilityKey;
    
    [SerializeField] private AbilityUI _abilityUI; //скрипт привязанный к UI части абилки


    private void Awake()
    {
        _ability = null;
        abilityKey.action.performed += AbilityExecute;
    }

    private void AbilityExecute(InputAction.CallbackContext obj)
    {
        if (_ability == null) return;
        _ability.Execute(gameObject);
    }

    private void Update()
    {
        if (_ability == null) return;
        _abilityUI.SetCooldownAmount(_ability.getCooldownAmount());
    }

    public void setAbility(Type ability)
    { 
        var abilities = GetComponentsInChildren<IAbility>();

        foreach (var abil in abilities)
        {
            if (abil.GetType() == ability)
            {
                _ability = abil;
            }
        }
        
        _abilityUI.SetAbilitySprite(_ability.getIcon());
    }

    public IAbility getAbility() { return this._ability; }
    
    private void OnDestroy()
    {
        abilityKey.action.performed -= AbilityExecute;
    }
}
