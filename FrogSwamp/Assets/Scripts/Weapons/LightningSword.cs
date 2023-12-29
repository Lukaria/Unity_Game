using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class LightningSword : Weapon
{
    [SerializeField] private float _strikeChance = 0.2f;

    [SerializeField] private GameObject _effect;

    [SerializeField] private float _strikeDamage = 1f;

    [SerializeField] private float _effectLifeTime = 3f;

    private AudioSource _audioSource;
    

    public override void OnEnable()
    {
        base.OnEnable();
        _playerAttack.OnAttack += LightningStrike;
        _audioSource = GetComponent<AudioSource>();
    }

    private void LightningStrike(GameObject obj)
    {
        if(_strikeChance < Random.Range(0f, 1f))
            return;
        Health enemyHealth = obj.GetComponent<Health>();
        if (enemyHealth.GetCurrentHealth() > 0)
        {
            enemyHealth.TakeDamage(_strikeDamage);
            StartCoroutine(InitAndDestroyEffect(obj));
        }
        
    }

    IEnumerator InitAndDestroyEffect(GameObject enemy)
    {
        GameObject obj = Instantiate(_effect, enemy.transform.position, Quaternion.identity);
        _audioSource.Play();
        yield return new WaitForSeconds(_effectLifeTime);
        Destroy(obj);
    }

    public override void OnDisable()
    {
        base.OnDisable();
        
        _playerAttack.OnAttack -= LightningStrike;
    }
    
    private void OnDestroy()
    {
        _playerAttack.OnAttack -= LightningStrike;
    }
}
