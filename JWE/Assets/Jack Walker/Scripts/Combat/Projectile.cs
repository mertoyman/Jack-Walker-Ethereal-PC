using UnityEngine;

namespace FPS.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] GameObject impactEffect = null;
        [SerializeField] GameObject[] destroyOnHit = null;
        [SerializeField] float headDamage = 5;
        [SerializeField] float bodyDamage = 5;
        [SerializeField] float otherDamage = 5;
        [SerializeField] float playerDamage = 20;
        [SerializeField] float lifeAfterImpact = 1;
        [SerializeField] float speed = 500;
        [SerializeField] bool playersProjectile;

        public PlayerHealth target;

        private void Awake()
        {
            if(playersProjectile) return;

            target = FindObjectOfType<PlayerHealth>();
            
        }

        private void Start()
        {
            if (!playersProjectile)
            {
                transform.LookAt(target.transform);
            }
        }

        private void Update()
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        public void SetTarget(PlayerHealth target)
        {
            this.target = target;
        }

        private void OnTriggerEnter(Collider other)
        {

            if (other.gameObject.layer == LayerMask.NameToLayer("Ignore Raycast"))
                return;

            if (other.GetComponentInParent<EnemyHealth>())
            {
                var enemyHealth = other.GetComponentInParent<EnemyHealth>();
                if (other.CompareTag("EnemyHead"))
                {
                    enemyHealth.TakeDamage(headDamage);
                }
                else if (other.CompareTag("EnemyBody"))
                {
                    enemyHealth.TakeDamage(bodyDamage);
                }
                else if (other.CompareTag("EnemyOthers"))
                {
                    enemyHealth.TakeDamage(otherDamage);
                }
            }

            if (other.GetComponentInParent<PlayerHealth>())
            {
                var playerHealth = other.GetComponentInParent<PlayerHealth>();
                playerHealth.TakeDamage(playerDamage);
            }

            //foreach (GameObject toDestroy in destroyOnHit)
            //{
            //    Destroy(toDestroy);
            //}

            if (impactEffect != null)
            {
                var impact = Instantiate(impactEffect, transform.position, Quaternion.identity);
                Destroy(impact.gameObject, 1);
            }

            Destroy(gameObject, lifeAfterImpact);
        }
    }
}
