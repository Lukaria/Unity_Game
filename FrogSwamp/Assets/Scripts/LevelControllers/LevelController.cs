using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class LevelController : MonoBehaviour
{
    [Inject] protected KillCounter _killCounter;

    [Inject] protected PlayerMovementScript _playerMovement;

    [SerializeField] private GameObject _winCanvas;

    public virtual void Awake()
    {
        _killCounter.OnAllKilled += WinLevel;
    }

    public void WinLevel()
    {
        _playerMovement.gameObject.GetComponent<PlayerInput>().DeactivateInput();
        _winCanvas.GetComponent<WinMenu>().WinGame();
    }
    
    void OnDestroy()
    {
        _killCounter.OnAllKilled -= WinLevel;
    }
}