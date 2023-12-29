
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class VampireAura : Ability
{
    [SerializeField] private VampireAuraStats _vampireAuraContext;

    private GameObject _effect;

    private PlayerAttack playerAttack;
    private PlayerHealth playerHealth;
    private void Awake()
    {
        ConvertContext(_vampireAuraContext);
    }

    public void FixedUpdate()
    {
        if (_effect)
            _effect.transform.position = playerHealth.gameObject.transform.position;
    }
    public override void Activate(GameObject go)
    {
        base.Activate(go);
        
        playerAttack = go.GetComponent<PlayerAttack>();
        playerHealth = go.GetComponent<PlayerHealth>();
        
        _effect = Instantiate(_vampireAuraContext.Effect,  
            playerHealth.gameObject.transform.position,
            Quaternion.identity);
        playerAttack.OnAttack += LifeSteal;
    }
    private void LifeSteal(GameObject obj)
    {
        playerHealth.AddHealth(playerAttack.GetAttackDamage());
    }
    public override void Cooldown()
    {
        base.Cooldown();
        playerAttack.OnAttack -= LifeSteal;
        Destroy(_effect);
    }
}