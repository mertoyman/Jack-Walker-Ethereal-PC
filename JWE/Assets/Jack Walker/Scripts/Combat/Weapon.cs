using UnityEngine;

namespace FPS.Combat
{
    [RequireComponent(typeof(AudioSource))]
    public class Weapon : MonoBehaviour
    {
        [SerializeField] GameObject projectilePrefab = null;
        [SerializeField] AudioSource audioSource;

        RaycastHit hit;

        public void LaunchProjectile(Vector3 launchPosition)
        {
            if (Physics.Raycast(launchPosition, Vector3.forward, out hit, 100f))
            {
                GameObject projectileInstance = Instantiate(projectilePrefab, launchPosition, transform.rotation) as GameObject;
                audioSource.Play();                
            }
        }

        public void LaunchProjectile(Vector3 launchPosition, PlayerHealth target)
        {
            if (Physics.Raycast(launchPosition, Vector3.forward, out hit, 100f))
            {
                GameObject projectileInstance = Instantiate(projectilePrefab, launchPosition, transform.rotation) as GameObject;
                projectileInstance.GetComponent<Projectile>().SetTarget(target);
                audioSource.Play();
            }
        }
    }
}