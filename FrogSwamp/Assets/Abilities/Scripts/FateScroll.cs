using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FateScroll : Ability
{
    [SerializeField] private FateScrollStats _fateScrollContext;

    private PlayerInput playerInput;
    private void Awake()
    {
        ConvertContext(_fateScrollContext);
    }

    public override void Activate(GameObject go)
    {
        base.Activate(go);

        PlayerHealth ph = go.GetComponent<PlayerHealth>();

        ph.AddMaxHealth(_fateScrollContext.Heal);
        
        _audioManager.Play(_fateScrollContext.Sound);
        
    }
}
