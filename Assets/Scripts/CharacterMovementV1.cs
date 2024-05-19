using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovementV1 : MonoBehaviour
{
    // Start is called before the first frame update

    private PlayerInputsV1 _playerInput;
    private CharacterController _characterController;
    private Animator _animator;
    private ObjectPush _objectPush;
    [SerializeField] private Transform _characterModel;

    [Header("Player Control Variables")]
    [Range(4, 8)] [SerializeField] private float _rotationFactor = 0.6f;
    [SerializeField] private float _walkSpeed = 1.5f;
    [SerializeField] private float _pushSpeed = 1.0f;

    [SerializeField] private float _gravity = -9.82f;
    //[SerializeField] private float _gravityFallMultiplier = 1.5f;
    //[SerializeField] private float _maxFallSpeed = -20.0f;

    [Header("Display for Debug Values")]
    [SerializeField] private Vector2 _currentMovementInput;
    [SerializeField] private Vector3 _appliedMovement;
    private Vector3 _cameraRelativeAppliedMovement;

    [SerializeField] private bool _isMovementPressed;
    [SerializeField] private bool _isPushPressed;
    [SerializeField] private bool _isFallenOver;
    //animation refs
    //private string _currentAnimationName = "";
    //private string _lastSubstateAnitmationName = "";



    void Awake()
    {
        _playerInput = new PlayerInputsV1();
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
        _objectPush = GetComponent<ObjectPush>();
        
        _appliedMovement.y = _gravity;

        _playerInput.BasicInputs.Move.started += context =>
        {
            Debug.Log(string.Format("Input is: {0}", context.ReadValue<Vector2>()));
            _playerInput.BasicInputs.Move.started += OnMovementInput;
            _playerInput.BasicInputs.Move.canceled += OnMovementInput;
            _playerInput.BasicInputs.Move.performed += OnMovementInput;

            _playerInput.BasicInputs.Push.started += OnPushInput;
            //_playerInput.BasicInputs.Push.canceled += OnPushInput;
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (_isFallenOver) 
        {
            return;
        }

        RotateCharacter();
        SetAnimation();
        SetAppliedMovement();
        _characterController.Move(_appliedMovement * Time.deltaTime);
        
    }

    private void OnMovementInput(InputAction.CallbackContext context)
    {
        _currentMovementInput = context.ReadValue<Vector2>();

        //check if there is any input, and assign to bool.
        _isMovementPressed = _currentMovementInput.x != 0 || _currentMovementInput.y != 0;

        //Context.AppliedMovementZ = Context.CurrentMovementInput.y * Context.WalkSpeed;
    }

    private void OnPushInput(InputAction.CallbackContext context) 
    {
        //_isPushPressed = context.ReadValueAsButton();

        _isPushPressed = !_isPushPressed;


        _objectPush.IsPushing = _isPushPressed;  
    }


    private void SetAppliedMovement() 
    {
        if (_isPushPressed)
        {

            _appliedMovement.x = _currentMovementInput.x * _pushSpeed;
            _appliedMovement.z = _currentMovementInput.y * _pushSpeed;
        }

        else
        {
            _appliedMovement.x = _currentMovementInput.x * _walkSpeed;
            _appliedMovement.z = _currentMovementInput.y * _walkSpeed;
        }
    }

    private void RotateCharacter()
    {
        //the change in position the character should point to.
        Vector3 positionToLookAt = new Vector3(_appliedMovement.x, 0, _appliedMovement.z);
        //positionToLookAt.y = 0;
        positionToLookAt.Normalize();

        Quaternion currentRotation = transform.rotation;

        if (_isMovementPressed)
        {
            if (positionToLookAt.sqrMagnitude <= 0)
            {
                return;
            }

            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, _rotationFactor * Time.deltaTime);
        }

    }

    

    private void SetAnimation() 
    {
        
        if (_isMovementPressed && !_isPushPressed)
        {
            //_animator.CrossFade seems useful. also needs a check for the current state.
            _animator.Play("walk");
        }

        else if (_isMovementPressed && _isPushPressed) 
        {
            _animator.Play("push");
        }
        else if (!_isMovementPressed)
        {
            _animator.Play("idle");
        }

    }

    public void FallOver() 
    {
        if (!_isFallenOver) 
        {
            _isFallenOver = true;
            _animator.Play("fallen over");
            Invoke("StoodUp", 5.2f);
        }

    }

    public void StoodUp() 
    {
        _isFallenOver = false;
    }


    private void OnEnable()
    {
        _playerInput.BasicInputs.Enable();
    }

    private void OnDisable()
    {
        _playerInput.BasicInputs.Disable();
    }
}
