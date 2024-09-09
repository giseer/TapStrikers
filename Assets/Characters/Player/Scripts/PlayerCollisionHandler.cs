using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerCollisionHandler : MonoBehaviour
{
    [HideInInspector] public Vector3 groundWorldPosition;
    [HideInInspector] public Collider lastColliderTriggered;

    private PlayerActions _playerActions;
    private PlayerMovement _playerMovement;

    private void OnEnable()
    {
        _playerActions.touch.action.Enable();
    }

    private void Awake()
    {
        _playerActions = GetComponent<PlayerActions>();
        _playerMovement= GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("RotationGround"))
        {
            _playerActions.actionToDo = PlayerActions.ActionToDo.Rotate;
            _playerMovement.alreadyRotatedInThisRotationGround = false;
            groundWorldPosition = col.gameObject.transform.position ;
        }
        if (col.CompareTag("EnemyHitbox") || col.CompareTag("PerseurHitbox"))
        {
            AudioManager.Instance.PlaySfx("FailSound");
            GameManager.ResetGame();
        }
        if (col.CompareTag("SlowerGround"))
        {
            _playerMovement.speed /= 2;
        }
        if(col.CompareTag("EnemyHurtbox") || col.CompareTag("StatueHurtbox"))
        {
            lastColliderTriggered = col;
            _playerActions.actionToDo = PlayerActions.ActionToDo.Attack;
            _playerMovement.isAvailableToJump = false;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("SlowerGround"))
        {
            _playerMovement.speed *= 2;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        _playerMovement.isAvailableToJump = true;
    }

    public void Reset()
    {
        groundWorldPosition = Vector3.zero;
    }

    private void OnDisable()
    {
        _playerActions.touch.action.Disable();
    }
}
