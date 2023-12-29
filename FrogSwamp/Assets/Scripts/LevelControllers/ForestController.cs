using UnityEngine;
using Zenject;

public class ForestController : LevelController
{

    [Inject] protected AudioManager _audioManager;
    private bool _isMusicPlayed = false;

    private void FixedUpdate()
    {
        if (_isMusicPlayed) return;
        
        _audioManager.Play("ForestMusic");
        _isMusicPlayed = true;
    }
}
