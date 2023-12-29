using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private float _damage = 2f;
    
    [SerializeField] private float _lifeTime = 10f;

    private void FixedUpdate()
    {
        if (_lifeTime <= 0)
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
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

        if (playerHealth)
        {
            playerHealth.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}
