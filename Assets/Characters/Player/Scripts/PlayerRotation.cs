using System;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    private PlayerMovement _playerMovement;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    public void Rotate()
    {
        if (_playerMovement.alreadyRotatedInThisRotationGround)
        {
            return;
        }
        
        switch (_playerMovement.facing)
        {
            case PlayerMovement.Facing.Left:
                transform.Rotate(Vector3.up, -90f);
                _playerMovement.facing = PlayerMovement.Facing.Right;
                break;
            
            case PlayerMovement.Facing.Right:
                transform.Rotate(Vector3.up, 90f);
                _playerMovement.facing = PlayerMovement.Facing.Left;
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }        
        
        _playerMovement.alreadyRotatedInThisRotationGround = true;
        GameManager.IncrementNumberPassedObstacles();
    }
}