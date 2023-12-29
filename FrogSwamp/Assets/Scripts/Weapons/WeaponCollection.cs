using System;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;
using Random = System.Random;
public class WeaponCollection : Interactable
{
    [SerializeField] private List<GameObject> _weapons = new List<GameObject>();
    
    [SerializeField] private int index = 0;
    
    [SerializeField] private bool _customIndex = false;

    [SerializeField] private string name;

    [SerializeField] private bool _isByName;
    
    private PlayerInteract playerInteractScript = null;
    private PlayerWeapon playerWeapon = null;

    [Inject] protected AudioManager _audioManager;
    private void Awake()
    {
        SetActivatedByPlayer(true);
        
        if (!_customIndex)
        {
            Random rnd = new Random();
            index = rnd.Next(_weapons.Count);
        }
        
        foreach (var weapon in _weapons)
        {
            weapon.SetActive(false);
        }
        
        _weapons[index].SetActive(true);
    }
    
    public override void Interact()
    {
        base.Interact();

        int lastWeapon = playerWeapon.GetWeaponCurrentId();

        if (lastWeapon == index) return;
        
        if (_isByName)
        {
            playerWeapon.SetActiveWeapon(gameObject.name);
        }
        else
        {
            playerWeapon.SetActiveWeapon(index);
        }

        _weapons[lastWeapon].SetActive(true);
        _weapons[index].SetActive(false);
        index = lastWeapon;
        
        _audioManager.Play("WeaponCollection");
    }
    private void OnTriggerEnter(Collider other)
    {
        playerInteractScript = other.GetComponent<PlayerInteract>();
        
        if(playerInteractScript == null) return;

        playerWeapon = other.GetComponent<PlayerWeapon>();

        playerInteractScript.OnInteract += Interact;
        
        playerInteractScript.SetHintCanvas(true);
        
    }
    private void OnTriggerExit(Collider other)
    {
        if(playerInteractScript == null) return;

        playerInteractScript.OnInteract -= Interact;
        playerInteractScript.SetHintCanvas(false);
        
        playerInteractScript = null;
        playerWeapon = null;
    }
}
