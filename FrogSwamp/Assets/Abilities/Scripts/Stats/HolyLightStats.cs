using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[CreateAssetMenu(fileName ="HolyLightStats", menuName = "Abilities/HolyLightStats")]
public class HolyLightStats : AbilityContextGeneral
{
    
    [SerializeField] private float _damage = 1;
    [SerializeField] private int _tickCount = 2;
    [SerializeField] private float _waitTime = 2;
    [SerializeField] private float _abilityRadius = 2;
    [SerializeField] private GameObject _effect;

    public float Damage => _damage;
    public int TickCount => _tickCount;
    public float WaitTime => _waitTime;
    public float AbilityRadius => _abilityRadius;

    public GameObject Effect => _effect;
}
