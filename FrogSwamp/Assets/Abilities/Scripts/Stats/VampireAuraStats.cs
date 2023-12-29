using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="VampireAuraStats", menuName = "Abilities/VampireAuraStats")]
public class VampireAuraStats : AbilityContextGeneral
{
    [SerializeField] private GameObject _effect;
    
    public GameObject Effect => _effect;
}
