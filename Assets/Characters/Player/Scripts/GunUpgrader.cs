using UnityEngine;
using Random = UnityEngine.Random;

    public class GunUpgrader : MonoBehaviour
    {
        [SerializeField] private GameObject gunObject;

        [SerializeField] private float incrementUpgradePercentagePerDestroyedStatueNumber = 3.0f;

        public float initialGunUpgradePercentage = 20.0f;
    
        [HideInInspector] public int gunLevel = 1;

        [HideInInspector] public float gunUpgradePercentage;
    
        private Color _initialColor;
        private Color _color;
        private MeshRenderer _meshRenderer;
        
        private void Awake()
        {
            _meshRenderer = gunObject.GetComponent<MeshRenderer>();
        }

        private void Start()
        {
            _initialColor = gunObject.GetComponent<MeshRenderer>().material.color;
            gunUpgradePercentage = initialGunUpgradePercentage;
        }
    
        public void UpgradeGun()
        {
            if(CanUpgradeGun())
            {
                gunLevel++;
            }

            switch (gunLevel)
            {
                case 1:
                    _color = _initialColor;
                    break;
                case 2:
                    _color = Color.yellow;
                    break;

                case 3:
                    _color = Color.blue;
                    break;

                case 4:
                    _color = Color.red;
                    break;
            }

            _meshRenderer.material.color = _color;

        }

        private bool CanUpgradeGun()
        {
            var randomPercentageNumber = Random.Range( 1, 101);
            

            if (randomPercentageNumber <= gunUpgradePercentage)
            {
                gunUpgradePercentage = initialGunUpgradePercentage;

                return true;
            }

            gunUpgradePercentage += incrementUpgradePercentagePerDestroyedStatueNumber;

            return false;
        }

        public void Reset()
        {
            gunLevel = 1;
            gunUpgradePercentage = initialGunUpgradePercentage;
            _meshRenderer.material.color = _initialColor;
        }
    }