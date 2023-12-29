using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Zenject;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementScript : MonoBehaviour
{
    #region Variables: Movement

    private Vector2 _input;
    private CharacterController _characterController;
    private Vector3 _direction;

    [SerializeField] private float _speed = 10;

    #endregion
    #region Variables: Rotation

    [SerializeField] private float smoothTime = 0.05f;
    private float _currentVelocity;

    #endregion
    #region Variables: Gravity

    private float _gravity = -9.81f;
    [SerializeField] private float gravityMultiplier = 3.0f;
    private float _velocity;

    #endregion
    private Animator _animator;

    [Inject] private CameraScript cameraScript;


    private List<Matrix4x4> _rotationMaxtrices = new List<Matrix4x4>();//корректировка движения за счет умножения 
                                                                    // на нужную матрицу поворота

    [Inject] protected AudioManager _audioManager;

    private AudioSource _footstepSound;
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _footstepSound = GetComponent<AudioSource>();
        
        _rotationMaxtrices.Add(Matrix4x4.Rotate(Quaternion.Euler(0, 45,0)));
        _rotationMaxtrices.Add(Matrix4x4.Rotate(Quaternion.Euler(0, -45,0)));
        _rotationMaxtrices.Add(Matrix4x4.Rotate(Quaternion.Euler(0, -135,0)));
        _rotationMaxtrices.Add(Matrix4x4.Rotate(Quaternion.Euler(0, 135,0)));
    }


    private void Update()
    {
        ApplyGravity();
        ApplyRotation();

        if (!_animator.GetBool("Attack") && (_direction.x != 0 || _direction.z != 0))
        {
            _animator.SetFloat("Speed", 5.0f);
            _footstepSound.enabled = true;
        }
        else
        {
            _animator.SetFloat("Speed", 0.0f);
            _footstepSound.enabled = false;
            _direction.x = 0;
            _direction.z = 0;
        }
        
        ApplyMovement();
    }

    private void ApplyGravity()
    {
        if (_characterController.isGrounded && _velocity < 0.0f)
        {
            _velocity = -1.0f;
        }
        else
        {
            _velocity += _gravity * gravityMultiplier * Time.deltaTime;
        }

        _direction.y = _velocity;
    }

    private void ApplyRotation()
    {
        if (_input.sqrMagnitude == 0) return;

        var targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _currentVelocity, smoothTime);
        transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
    }

    private void ApplyMovement()
    {
        _characterController.Move(_direction * _speed * Time.deltaTime);
    }

    public void Move(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();
        _direction =  _rotationMaxtrices[cameraScript.GetCurrentOffsetNumber()]
                      * new Vector3(_input.x, 0.0f, _input.y);
    }

    public void SetSpeed(float value)
    {
        _speed = value;
    }

    public float GetSpeed()
    {
        return _speed;
    }
}