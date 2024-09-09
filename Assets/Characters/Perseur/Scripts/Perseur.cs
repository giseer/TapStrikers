using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
    
    public class Perseur : MonoBehaviour
    {
        private enum Facing
        {
            Left,
            Right
        }
        
        [SerializeField] private InputActionReference touch;
        [SerializeField] private float speed;
        [SerializeField] private Animator animator;

        private Vector3 _initialPosition;

        private bool _active = false;
        private bool _alreadyRotated = false;

        private Facing _facing = Facing.Right;

        private void OnEnable()
        {
            touch.action.Enable();
            GameManager.onResetGame.AddListener(Reset);
        }

        private void Start()
        {
            _initialPosition = transform.parent.position;
        }

        private void Update()
        {
            StartPlayingOnMouseClick();

            if (_active)
            {
                transform.parent.Translate(Vector3.back * (speed * Time.deltaTime));
            }
        }

        private void StartPlayingOnMouseClick()
        {
            if (touch.action.triggered)
            {
                animator.enabled = true;
                _active = true;
            }
        }

        private void TruncatePositionToKeepCenterAndRotate(Collider col)
        {
            var position = transform.parent.position;
            if (_facing == Facing.Right)
            {
                if (!(transform.parent.position.z < col.transform.position.z))
                {
                    return;
                }
                position = new Vector3(position.x, position.y, col.transform.position.z);
                transform.parent.Rotate(Vector3.up, 90f);
                _facing = Facing.Left;
                _alreadyRotated = true;
            }
            else if (_facing == Facing.Left)
            {
                if (!(transform.parent.position.x < col.transform.position.x))
                {
                    return;
                }
                position = new Vector3(col.transform.position.x, position.y, position.z);
                transform.parent.Rotate(Vector3.up, -90f);
                _facing = Facing.Right;
                _alreadyRotated = true;
            }
        }

        private void Reset()
        {
            if (_facing == Facing.Left)
            {
                transform.parent.Rotate(Vector3.up, -90f);
                _facing = Facing.Right;
            }
            transform.parent.position = _initialPosition;
            _alreadyRotated = false;
            _active = false;
            animator.enabled = false;
        }


        private void OnTriggerStay(Collider col)
        {
            if (col.tag.Equals("RotationGround") && !_alreadyRotated)
            {
                TruncatePositionToKeepCenterAndRotate(col);
            }
        }

        private void OnTriggerExit(Collider col)
        {
            if(col.tag.Equals("RotationGround"))
            {
                _alreadyRotated = false;
            }
        }

        private void OnDisable()
        {
            touch.action.Disable();
            GameManager.onResetGame.RemoveListener(Reset);
        }
    }
