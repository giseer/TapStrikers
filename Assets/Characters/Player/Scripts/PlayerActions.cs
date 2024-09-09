using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    public enum ActionToDo
    {
        Rotate,
        Jump,
        Attack,
        None
    }
    
    [Header("Input Actions")]
    public InputActionReference touch;
    [Space(20)]

    [Header("Events")]
    
    [Space(20)]

    [HideInInspector] public ActionToDo actionToDo = ActionToDo.Jump;

    [HideInInspector] public Animator animator;

    private ActionToDo _lastActionState;

    private PlayerMovement _movement;
    private PlayerCollisionHandler _collision;
    private PlayerJump _playerJump;
    private PlayerRotation _playerRotation;
    private PlayerAttack _playerAttack;

    private Rigidbody _rigidbody;

    private void OnEnable()
    {
        GameManager.onPauseMenu.AddListener(PausePlayerState);
        GameManager.onResumeGame.AddListener(ResumePlayerState);
    }

    private void Awake()
    {
        _movement = GetComponent<PlayerMovement>();
        _collision = GetComponent<PlayerCollisionHandler>();
        _playerJump = GetComponent<PlayerJump>();
        _playerRotation = GetComponent<PlayerRotation>();
        _playerAttack = GetComponent<PlayerAttack>();
        
        _rigidbody = GetComponent<Rigidbody>();
        
        animator = GetComponent<Animator>();
        
    }
    private void Update()
    {
        CheckAndPerformPlayerActions();

        if(transform.position.y < -10)
        {
            AudioManager.Instance.PlaySfx("FailSound");
            GameManager.ResetGame();
        }
    }

    private void CheckAndPerformPlayerActions()
    {
        Debug.Log(_movement.isRunning);
        if (_movement.isRunning)
        {
            _movement.RunPlayer();
        }

        if (touch.action.triggered)
        {            
            if (!_movement.alreadyRunning)
            {
                _movement.isRunning = true;
                _movement.alreadyRunning = true;
            }
            else
            {
                if (actionToDo == ActionToDo.Jump || actionToDo == ActionToDo.Rotate)
                {
                    PlayerDoAction();
                }
                else if (actionToDo == ActionToDo.Attack)
                {
                    if(_collision.lastColliderTriggered != null)
                    {
                        PlayerDoActionWithCollider(_collision.lastColliderTriggered);
                    }                    
                }
            }
        }
    }

    public void PlayerDoAction()
    {
        switch (actionToDo)
        {
            case ActionToDo.Jump:
                _playerJump.Jump(_rigidbody);
                break;

            case ActionToDo.Rotate:
                _playerRotation.Rotate();
                actionToDo = ActionToDo.Jump;
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void PlayerDoActionWithCollider(Collider col)
    {
        if (actionToDo == ActionToDo.Attack)
        {
            _playerAttack.Attack(col);
        }
    }

    private void PausePlayerState()
    {
        _lastActionState = actionToDo;
        actionToDo = ActionToDo.None;
    }

    private void ResumePlayerState()
    {
        actionToDo = _lastActionState;
    }

    public void Reset()
    {
        animator.Play("Idle"); 
        _playerAttack.Reset();
    }

    private void OnDisable()
    {
        GameManager.onPauseMenu.RemoveListener(PausePlayerState);
        GameManager.onResumeGame.RemoveListener(ResumePlayerState);
    }
}
