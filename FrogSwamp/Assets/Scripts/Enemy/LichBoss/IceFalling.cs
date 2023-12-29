using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using FixedUpdate = Unity.VisualScripting.FixedUpdate;

public class IceFalling : MonoBehaviour
{
    private PlayerHealth _playerHealth = null;
    private PlayerMovementScript _playerMovement = null;

    [SerializeField] private float _damage = 1f;

    [SerializeField] private float _lifeTime;
    
    [SerializeField] private float _waitTime;
    
    [SerializeField] private float _slowingPercent = 0.5f;
    private float _playerSpeed;

    private void FixedUpdate()
    {
        if (_lifeTime <= 0)
        {
            if(_playerMovement)
                _playerMovement.SetSpeed(_playerSpeed);
            Destroy(gameObject);
        }
        else
        {
            _lifeTime -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _playerHealth = other.GetComponent<PlayerHealth>();

        if (_playerHealth)
        {
            _playerMovement = other.GetComponent<PlayerMovementScript>();
            _playerSpeed = _playerMovement.GetSpeed();
            _playerMovement.SetSpeed(_playerSpeed * (1 - _slowingPercent));
            StartCoroutine(IceFallingEffect());
        }
    }
    
    IEnumerator IceFallingEffect()
    {
        while(_playerHealth)
        {
            _playerHealth.TakeDamageWithoutAnim(_damage);
            yield return new WaitForSeconds(_waitTime);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(_playerHealth) _playerHealth = null;
        
        if (_playerMovement)
        {
            _playerMovement.SetSpeed(_playerSpeed);
            _playerMovement = null;
        }
        StopCoroutine(IceFallingEffect());
    }
}
