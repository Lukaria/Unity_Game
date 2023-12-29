 using System;
using System.Collections;
using System.Collections.Generic;
 
 using UnityEngine;
 using UnityEngine.InputSystem;
 using Vector3 = UnityEngine.Vector3;
 using Zenject;


 public class CameraScript : MonoBehaviour
{
    [Inject] protected PlayerMovementScript playerMovementScript;
    [SerializeField] private Transform target;
    [SerializeField] private bool isCustomOffest;
    [SerializeField] private bool _isCameraRotated = true;
    
    
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothSpeed = 0.1f;
    
    [SerializeField] private InputActionReference cameraRotation;
    private bool _isPressedButton;

    private List<Vector3> _cameraRotationVectors = new List<Vector3>();
    private int _currentOffsetNumber = 0;

    public int GetCurrentOffsetNumber()
    {
        return _currentOffsetNumber;
    }
    
    private void Awake()
    {
        _isPressedButton = false;   
    }

    void Start()
    {
        if (!isCustomOffest)
        {
            offset = transform.position - target.position;
        }
        else
        {
            _cameraRotationVectors.Add(offset);
            _cameraRotationVectors.Add(new Vector3(-offset.x, offset.y, offset.z));
            _cameraRotationVectors.Add(new Vector3(-offset.x, offset.y, -offset.z));
            _cameraRotationVectors.Add(new Vector3(offset.x, offset.y, -offset.z));

            offset = _cameraRotationVectors[_currentOffsetNumber];
        }

        if (!target)
        {
            target = playerMovementScript.transform;
        }
    }

    private void Update()
    {
        if (cameraRotation.action.IsPressed())
        {
            if (!_isPressedButton)
            {
                _isPressedButton = true;
                UpdateOffset(cameraRotation.action.ReadValue<float>());
            }
            
        }
        else
        {
            _isPressedButton = false;
        }
    }

    void UpdateOffset(float value)
    {
        if(!_isCameraRotated) return;

        _currentOffsetNumber += (int)value;
        
        if (_currentOffsetNumber < 0)
            _currentOffsetNumber = 3;
        else if(_currentOffsetNumber > 3)
            _currentOffsetNumber = 0;

        offset = _cameraRotationVectors[_currentOffsetNumber];
    }
    void LateUpdate()
    {
        SmoothFollow();
    }

    private void SmoothFollow()
    {
        Vector3 targetPos = target.position + offset;
        Vector3 smoothFollow = Vector3.Lerp(transform.position,
        targetPos, smoothSpeed);

        transform.position = smoothFollow;
        transform.LookAt(target);
    }
}
