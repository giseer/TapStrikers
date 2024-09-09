using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    
    private PlayerMovement _playerMovement;
    private PlayerActions _playerActions;
    
    private Animator _animator;

    private void Awake()
    {
        _playerActions = GetComponent<PlayerActions>();
        _playerMovement = GetComponent<PlayerMovement>();
        _animator = GetComponent<Animator>();
    }

    public void Jump(Rigidbody playerRigidbody)
    {
        if (!_playerMovement.alreadyRunning || !_playerMovement.isAvailableToJump)
        {
            return;
        }
        playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        _animator.SetTrigger("isJumping");
        AudioManager.Instance.PlaySfx("JumpSound");
        _playerMovement.isAvailableToJump = false;
        GameManager.IncrementNumberPassedObstacles();
    }
}