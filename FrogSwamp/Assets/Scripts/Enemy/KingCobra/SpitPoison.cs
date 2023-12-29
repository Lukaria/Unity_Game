using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitPoison : MonoBehaviour
{
    [SerializeField] private float _damage = 1f;
    [SerializeField] private float _lifeTime = 10f;
    [SerializeField] private int _tickCount = 2;
    [SerializeField] private float _delay = 1f;
    void Awake()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_lifeTime < 0)
        {
            Destroy(gameObject);
        }
        else
        {
            _lifeTime -= Time.deltaTime;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealth =  other.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth)
        {
            StartCoroutine(PoisonDamage(playerHealth));
        }
       
    }

    IEnumerator PoisonDamage(PlayerHealth playerHealth)
    {
        for (int i = 0; i < _tickCount; ++i)
        {
            playerHealth.TakeDamage(_damage);
            yield return new WaitForSeconds(_delay);
        }
    }
}
