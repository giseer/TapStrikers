using UnityEngine;
using UnityEngine.Events;

public class PlayerAttack : MonoBehaviour
{
    public UnityEvent<int> onPlayerKill;
    
    private bool _canKill;

    [HideInInspector] public int playerKills;
    
    private Animator _animator;

    private PlayerActions _playerActions;
    private PlayerMovement _playerMovement;
    private GunUpgrader _gunUpgrader;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        
        _playerActions = GetComponent<PlayerActions>();
        _playerMovement = GetComponent<PlayerMovement>();
        _gunUpgrader = GetComponent<GunUpgrader>();
    }

    public void Attack(Collider col)
    {
        _animator.SetTrigger("Attack");
        
        AudioManager.Instance.PlaySfx("KillSound");

        var gunLevel = _gunUpgrader.gunLevel;
        
        if (col.CompareTag("EnemyHurtbox"))
        {
            if (gunLevel>0 && col.gameObject.transform.parent.gameObject.CompareTag("Barbarian"))
            {
                _canKill = true;
            }
            else if(gunLevel>1 && col.gameObject.transform.parent.gameObject.CompareTag("Rogue"))
            {
                _canKill = true;
            }
            else if(gunLevel>2 && col.gameObject.transform.parent.gameObject.CompareTag("Mage"))
            {
                _canKill = true;
            }
            else if(gunLevel>3 && col.gameObject.transform.parent.gameObject.CompareTag("King"))
            {
                _canKill = true;
            }

            if(_canKill)
            {
                _animator.SetTrigger("Attack");
                playerKills++;
                onPlayerKill.Invoke(playerKills);
                GameManager.IncrementNumberPassedObstacles();
                Destroy(col.gameObject.transform.parent.gameObject);
                _canKill = false;
            }
            else
            {
            }
                    
            _playerActions.actionToDo = PlayerActions.ActionToDo.Jump;
            _playerMovement.isAvailableToJump = true;
        }
        else if(col.CompareTag("StatueHurtbox"))
        {
            Destroy(col.gameObject.transform.parent.gameObject);
            _playerActions.actionToDo = PlayerActions.ActionToDo.Jump;
            _playerMovement.isAvailableToJump = true;
            if (gunLevel < 4)
            {
                _gunUpgrader.UpgradeGun();
            }
        }
    }

    public void Reset()
    {
        playerKills = 0;
        onPlayerKill.Invoke(playerKills);
        _gunUpgrader.Reset();
    }
}