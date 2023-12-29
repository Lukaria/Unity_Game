using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="BleedingStrikeStats", menuName = "Abilities/BleedingStrikeStats")]
public class BleedingStrikeStats : AbilityContextGeneral
{
    [SerializeField] private float _bleedingDamage;
    
    [SerializeField] private int _tickCount;
    [SerializeField] private float _waitTime;
    [SerializeField] private GameObject _effect;
    [SerializeField] private string _sound;
    
    public string Sound => _sound;

    public int TickCount => _tickCount;
    public float WaitTime => _waitTime;
    public float BleedingDamage => _bleedingDamage;
    public GameObject Effect => _effect;
}
