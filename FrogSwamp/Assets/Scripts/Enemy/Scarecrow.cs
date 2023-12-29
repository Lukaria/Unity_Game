using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scarecrow : EnemyController
{
    public override void Update()
    {
    }
    
    public override void Death()
    {
        GetComponent<CapsuleCollider>().enabled = false;
        _animator.SetTrigger("DieTrigger");
    }
}
