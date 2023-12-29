using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DefaultSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;

    [SerializeField] private bool _isSpawnedOnce = true;
    
    [SerializeField] private bool _isSpawned = false;

    [Inject] private DiContainer _diContainer;

    private void OnTriggerEnter(Collider other)
    {
        if(!_isSpawned){
            _diContainer.InstantiatePrefab(_gameObject, transform.position,
                Quaternion.Euler(0.0f, 0.0f, 0.0f), null);
            
            if(_isSpawnedOnce) 
                _isSpawned = true;
        }
    }
}
