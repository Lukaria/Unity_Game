using UnityEngine;
using Zenject;

public class BossRoomCloser : MonoBehaviour
{
    [SerializeField] private GameObject _wall;
    
    [Inject] protected AudioManager _audioManager;

    private bool _isTriggered = false;

    private void Awake()
    {
        _wall.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isTriggered)
        {
            _audioManager.Stop("DungeonMusic");
            _wall.SetActive(true);
            _audioManager.Play("BossMusic");
            _isTriggered = true;
        }
        
    }
}
