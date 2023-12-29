using System;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using Zenject;

public class DungeonController : LevelController
{
    [SerializeField] private GameObject _bossRoom;

    [SerializeField] private GameObject _door;

    [SerializeField] private GameObject _boss;
    
    [Inject] protected AudioManager _audioManager;

    private bool _roomActivated;
    private bool _isInit = false;
    
    public override void Awake()
    {
       
    }

    private void FixedUpdate()
    {
        if (_isInit) return;

        _audioManager.Play("DungeonMusic");
        DeactivateBossRoom();
        _killCounter.OnAllKilled += ActivateBossRoom;
        _boss.GetComponent<Lich>().OnDeath += WinLevel;
        _isInit = true;
    }

    private void ActivateBossRoom()
    {
        _bossRoom.SetActive(true);
        _boss.SetActive(true);
        _door.SetActive(false);
    }
    
    private void DeactivateBossRoom()
    {
        _bossRoom.SetActive(false);
        _boss.SetActive(false);
        _door.SetActive(true);
    }
    
    void OnDestroy()
    {
        _killCounter.OnAllKilled -= ActivateBossRoom;
        _boss.GetComponent<Lich>().OnDeath -= WinLevel;
    }
}
