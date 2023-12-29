using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private List<GameObject> _weapons = new List<GameObject>();

    private string _currentWeaponName;
    
    [SerializeField] private int _currentWeaponId = 0;
    void Awake()
    {
        _currentWeaponName = _weapons[_currentWeaponId].name;
    }

    public void SetActiveWeapon(string name)
    {
        for (int i = 0; i < _weapons.Count; ++i)
        {
            if (_weapons[i].name == name)
            {
                Debug.Log(i);
                _weapons[_currentWeaponId].SetActive(false);
                _weapons[i].SetActive(true);
                _currentWeaponId = i;
                _currentWeaponName = name;
                break;
            }
        }
    }

    public void SetActiveWeapon(int id)
    {
        if (id > _weapons.Count - 1)
        {
            id = _weapons.Count - 1;
        }
        
        _weapons[_currentWeaponId].SetActive(false);
        _weapons[id].SetActive(true);
        _currentWeaponId = id;
    }

    public int GetWeaponCurrentId()
    {
        return _currentWeaponId;
    }
}
