using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerCollisionHandler _collision;
    private PlayerActions _actions;
    private PlayerMovement _movement;

    private void OnEnable()
    {
        GameManager.onResetGame.AddListener(Reset);
        GameManager.onWinGame.AddListener(OnWinGame);
    }

    private void Awake()
    {
        _collision = GetComponent<PlayerCollisionHandler>();
        _actions = GetComponent<PlayerActions>();
        _movement = GetComponent<PlayerMovement>();
    }

    private void OnWinGame()
    {
        _movement.StopRunPlayer();
    }

    public void Reset()
    {
        _collision.Reset();
        _actions.Reset();
        _movement.Reset();
    }

    private void OnDisable()
    {
        GameManager.onResetGame.RemoveListener(Reset);
        GameManager.onWinGame.RemoveListener(OnWinGame);
    }
}