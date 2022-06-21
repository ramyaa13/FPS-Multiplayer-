using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float AnimBlendSpeed = 8.9f;
    [SerializeField]
    private Transform CameraRoot;
    [SerializeField]
    private Transform Camera;
    [SerializeField]
    private float UpperLimit = -40f;
    [SerializeField]
    private float BottomLimit = 70f;
    [SerializeField]
    private float MouseSensitivity = 21.9f;
    [SerializeField]
    private GameObject CanvasUI;

    private Rigidbody _playerRigidBody;
    private InputManager _inputManager;
    private Animator _animator;
    private bool _hasAnimator;

    private int _xVelHash;
    private int _yVelHash;

    private float _xRotation;

    private const float _walkSpeed = 2f;
    private const float _runSpeed = 6f;

    private Vector2 _CurrentVelocity;

    private PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        _hasAnimator = TryGetComponent<Animator>(out _animator);
        _playerRigidBody = GetComponent<Rigidbody>();
        _inputManager = GetComponent<InputManager>();

        _xVelHash = Animator.StringToHash("X_Velocity");
        _yVelHash = Animator.StringToHash("Y_Velocity");

        view = GetComponent<PhotonView>();

        if(!view.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(CanvasUI);
        }

    }

    private void FixedUpdate()
    {
        if (view.IsMine)
        {
            Move();
        }
    }
    private void LateUpdate()
    {
        if (view.IsMine)
        {
            CamMovements();
        }
    }
    private void Move()
    {
        
        if (!_hasAnimator) return;

        float targetSpeed = _inputManager.Run ? _runSpeed : _walkSpeed;
        if(_inputManager.Move == Vector2.zero) targetSpeed = 0f;

        _CurrentVelocity.x = Mathf.Lerp(_CurrentVelocity.x, _inputManager.Move.x * targetSpeed, AnimBlendSpeed * Time.fixedDeltaTime);
        _CurrentVelocity.y = Mathf.Lerp(_CurrentVelocity.y, _inputManager.Move.y * targetSpeed, AnimBlendSpeed * Time.fixedDeltaTime);

        var xVelDifference = _CurrentVelocity.x - _playerRigidBody.velocity.x;
        var zVelDifference = _CurrentVelocity.y - _playerRigidBody.velocity.z;

        _playerRigidBody.AddForce(transform.TransformVector(new Vector3(xVelDifference,0, zVelDifference)), ForceMode.VelocityChange);

        _animator.SetFloat(_xVelHash, _CurrentVelocity.x);
        _animator.SetFloat(_yVelHash, _CurrentVelocity.y);
    }


    private void CamMovements()
    {
        if (!_hasAnimator) return;

        var MouseX = _inputManager.Look.x;
        var MouseY = _inputManager.Look.y;
        Camera.position = CameraRoot.position;

        _xRotation -= MouseY * MouseSensitivity * Time.smoothDeltaTime;
        _xRotation = Mathf.Clamp(_xRotation, UpperLimit, BottomLimit);

        Camera.localRotation = Quaternion.Euler(_xRotation, 0, 0);
       // _playerRigidBody.MoveRotation(_playerRigidBody.rotation * Quaternion.Euler(0, MouseX * MouseSensitivity * Time.smoothDeltaTime, 0));
        transform.Rotate(Vector3.up, MouseX * MouseSensitivity * Time.deltaTime);
    }
}
