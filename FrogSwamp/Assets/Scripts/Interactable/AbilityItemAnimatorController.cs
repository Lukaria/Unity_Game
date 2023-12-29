using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AbilityItemAnimatorController : MonoBehaviour
{
    private Animation _animation;
    
    private bool _isPlaying = false;

    private float _animationTime = 2.0f;

    private Animation anim;
    
    private void Awake()
    {
        _animation = gameObject.GetComponent<Animation>();
    }

    void FixedUpdate()
    {
        if (!_isPlaying)
        {
            _animation.Play("AbilityItemAnim" + Random.Range(1, 3));
            StartCoroutine(AnimationPlaying(_animationTime));
        }
    }

    IEnumerator AnimationPlaying(float animationTime)
    {
        _isPlaying = true;
        yield return new WaitForSeconds(animationTime);
        _isPlaying = false;
        
    }
}
