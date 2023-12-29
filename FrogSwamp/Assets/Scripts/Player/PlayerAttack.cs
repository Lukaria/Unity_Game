using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

using Zenject;

public class PlayerAttack : MonoBehaviour
{
    [Inject] private PauseMenu pauseMenu;

    #region SphereCast

    [SerializeField] private Transform HitPoint;
    [SerializeField] private LayerMask enemyLayerMask;
    private RaycastHit[] hits = new RaycastHit[3];

    #endregion
    
    public event Action<GameObject> OnAttack; //для эффектов атаки на противнике

    [SerializeField] private float _attackDamage;

    [SerializeField] private InputActionReference _attack;
    [SerializeField] private InputActionReference _mousePos;

    private Animator _animator;
    
    private Camera _camera;
    
    [Inject] protected AudioManager _audioManager;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _attack.action.performed += Attack;
    }

    void Start()
    {
        _camera = Camera.main;
        
    }
    private void Attack(InputAction.CallbackContext obj)
    {
        if (PauseMenu.isGamePaused()) return;
        
        _animator.SetTrigger("AttackTrigger");


        var rayHit = _camera.ScreenPointToRay((Vector3)_mousePos.action.ReadValue<Vector2>());
        Physics.Raycast(rayHit, out var hit);
        var point = hit.point;
        point.y = transform.position.y;
        transform.forward = (point-transform.position).normalized;
    }

    
    public void HitMeleeAttack()
    {
        _audioManager.Play("PlayerAttack");
        
        var size = Physics.SphereCastNonAlloc(HitPoint.position, 0.4f, 
            HitPoint.forward, hits, 0f, enemyLayerMask);
        if (size > 0)
        {
            Health healthScript= hits[0].transform.gameObject.GetComponent<Health>();
            if (healthScript != null)
            {
                OnAttack?.Invoke(hits[0].transform.gameObject);
                
                healthScript.TakeDamage(_attackDamage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(HitPoint.position, 0.4f);
    }

    public void SetAttackDamage(float attackDamage)
    {
        _attackDamage = attackDamage;
    }

    public float GetAttackDamage()
    {
        return _attackDamage;
    }

    public Vector3 GetHitPoint()
    {
        return HitPoint.position;
    }

    public void OnDestroy()
    {
        _attack.action.performed -= Attack;
    }
}

