using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="FateScrollStats", menuName = "Abilities/FateScrollStats")]
public class FateScrollStats : AbilityContextGeneral
{
    [SerializeField] private float _heal = 1;
    
    [SerializeField] private string _sound;
    
    public string Sound => _sound;
    public float Heal => _heal;
}
