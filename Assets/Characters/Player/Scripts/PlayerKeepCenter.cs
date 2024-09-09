using System;
using UnityEngine;

public class PlayerKeepCenter : MonoBehaviour
{
    private PlayerCollisionHandler _playerCollision;
    private PlayerMovement _playerMovement;
    
    [SerializeField] private float centerSpeed;
    
    private float _desiredX;
    private float _desiredZ;

    private void Awake()
    {
        _playerCollision = GetComponent<PlayerCollisionHandler>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    public void KeepPlayerCenter(Transform transform, PlayerMovement.Facing playerFacing)
    {
        float distance;
        float currentX;
        float currentZ;

        currentX = transform.position.x;
        currentZ = transform.position.z;

        switch (playerFacing)
        {
            case PlayerMovement.Facing.Left:
            {
                _desiredZ = _playerCollision.groundWorldPosition.z;
                distance = _desiredZ - currentZ;
                var expectedCenteringDistance = centerSpeed * Time.deltaTime;
                currentZ += 
                    Mathf.Sign(distance) * 
                    Mathf.Min(Mathf.Abs(distance), expectedCenteringDistance);
                break;
            }
            case PlayerMovement.Facing.Right:
                _desiredX = _playerCollision.groundWorldPosition.x;
                distance = _desiredX - currentX;
                currentX += 
                    Mathf.Sign(distance) * 
                    Mathf.Min(Mathf.Abs(distance), centerSpeed * Time.deltaTime);
                break;
        }
        transform.position = new Vector3(currentX, transform.position.y, currentZ);
    }
}