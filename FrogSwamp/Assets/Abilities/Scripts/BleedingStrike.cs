using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BleedingStrike : Ability
{
    [SerializeField] private BleedingStrikeStats _bleedingStrikeContext;

    private GameObject _effect = null;
    
    private PlayerAttack _playerAttack;
    private void Awake()
    {
        ConvertContext(_bleedingStrikeContext);
    }
    
    public void FixedUpdate()
    {
        if(_effect)
            _effect.transform.position = _playerAttack.GetHitPoint();
    }

    
    public override void Activate(GameObject go)
    {
        base.Activate(go);
        _audioManager.Play(_bleedingStrikeContext.Sound);
        
        _playerAttack = go.GetComponent<PlayerAttack>();

        _playerAttack.OnAttack += BleedingStrikeEffect;
        _effect = Instantiate(_bleedingStrikeContext.Effect);
    }

    private void BleedingStrikeEffect(GameObject obj)
    {
        Health healthScript = obj.GetComponent<Health>();
        if (healthScript == null) return;
        
        StartCoroutine(BleedingStrikeDOT(healthScript));
    }

    IEnumerator BleedingStrikeDOT(Health health)
    {
        
        for (int i = 0; i < _bleedingStrikeContext.TickCount; ++i)
        {
            if (health.GetCurrentHealth() > 0)
            {
                health.TakeDamageWithoutAnim(_bleedingStrikeContext.BleedingDamage);
                yield return new WaitForSeconds(_bleedingStrikeContext.WaitTime);
            }
        }
    }
    
    public override void Cooldown()
    {
        base.Cooldown();
        _playerAttack.OnAttack -= BleedingStrikeEffect;
        Destroy(_effect);
        _effect = null;
    }
}
