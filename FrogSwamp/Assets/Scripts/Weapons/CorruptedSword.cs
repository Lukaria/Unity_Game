using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptedSword : Weapon
{
        [SerializeField] private float _strikeChance = 0.2f;
    
        [SerializeField] private GameObject _effect;

        [SerializeField] private float _effectLifeTime = 3f;

        [SerializeField] private float _slowingPercent = 0.2f;
        
        [SerializeField] private float _curseDamage = 0.001f;
    
        public override void OnEnable()
        {
            base.OnEnable();
            _playerAttack.OnAttack += CurseStrike;
        }
    
        private void CurseStrike(GameObject obj)
        {
            if(_strikeChance < Random.Range(0f, 1f))
                return;
            
            EnemyController enemy = obj.GetComponent<EnemyController>();
            
            GetComponent<AudioSource>().Play();
            StartCoroutine(InitAndDestroyEffect(enemy));
        }

        private void CurseEffect(GameObject obj)
        {
            Health health = obj.GetComponent<Health>();
            if (health.GetCurrentHealth() > 0)
            {
                health.TakeDamageWithoutAnim(_curseDamage);
            }
        }

        IEnumerator InitAndDestroyEffect(EnemyController enemy)
        {
            enemy.effectsHolder += CurseEffect;
            enemy.SetCurrentSpeed(enemy.GetCurrentSpeed() * (1f - _slowingPercent));
            GameObject effect = Instantiate(_effect,
                enemy.transform.position + new Vector3(0f, 2f, 0f),
                Quaternion.identity);
            yield return new WaitForSeconds(_effectLifeTime);
            enemy.effectsHolder -= CurseEffect;
            enemy.RestoreSpeed();
            Destroy(effect);
        }
    
        public override void OnDisable()
        {
            base.OnDisable();
            
            _playerAttack.OnAttack -= CurseStrike;
        }
        
        private void OnDestroy()
        {
            _playerAttack.OnAttack -= CurseStrike;
        }
}
