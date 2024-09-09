using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    public enum Facing
    {
        Left,
        Right
    }

    [HideInInspector] public Vector3 initialPosition;
    
    public float speed;
    [HideInInspector] public float initalSpeed;
    [HideInInspector] public bool isRunning;
    [HideInInspector] public bool alreadyRunning = false;
    [HideInInspector] public bool isAvailableToJump = true;
    [HideInInspector] public Facing facing = Facing.Right;
    [HideInInspector] public bool alreadyRotatedInThisRotationGround = false;
    [Space(20)]
    
    private PlayerActions _playerActions;
    private PlayerKeepCenter _playerKeepCenter;


    private void OnEnable()
    {
        _playerActions.touch.action.Enable();
    }
    private void Awake()
    {
        _playerActions = GetComponent<PlayerActions>();
        _playerKeepCenter = GetComponent<PlayerKeepCenter>();
    }

    private void Start()
    {
        initialPosition = transform.position;
    }

    public void RunPlayer()
    {
        Debug.Log("corriendo!");
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
        KeepPlayerCenter();
        _playerActions.animator.SetBool("isRunning", true);
    }
    
    private void KeepPlayerCenter()
    {
        _playerKeepCenter.KeepPlayerCenter(transform, facing);
    }
    
    public void StopRunPlayer()
    {
        _playerActions.animator.SetBool("isRunning", false);
        isRunning = false;
    }

    public void Reset()
    {
        StopRunPlayer();
        alreadyRunning = false;
        transform.position = initialPosition;
        isAvailableToJump = true;
        
        if (facing == Facing.Right)
        {
            return;
        }
        
        transform.Rotate(Vector3.up, -90f);
        facing = Facing.Right;
    }

    private void OnDisable()
    {
        _playerActions.touch.action.Disable();
    }
}
