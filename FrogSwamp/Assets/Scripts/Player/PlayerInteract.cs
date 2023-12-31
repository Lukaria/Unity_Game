using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerInteract : MonoBehaviour
{

    [SerializeField] private InputActionReference _interact;

    [SerializeField] private GameObject _hintCanvas;
    
    public event Action OnInteract;

    void Awake()
    {
        _interact.action.performed += OnInteractKeyPressed;
        _hintCanvas = GameObject.Find("HUD").transform.GetChild(2).gameObject;
        _hintCanvas.SetActive(false);
    }

    private void OnInteractKeyPressed(InputAction.CallbackContext context)
    {
        OnInteract?.Invoke();
    }

    public void SetHintCanvas(bool setter)
    {
        _hintCanvas.SetActive(setter);
    }
    
    void OnDestroy()
    {
        _interact.action.performed -= OnInteractKeyPressed;
    }

}
