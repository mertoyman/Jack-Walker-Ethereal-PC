using FPS.Combat;
using UnityEngine;

public class NPCWeaponShoot : MonoBehaviour
{
    [SerializeField] Weapon npcWeapon;
    [SerializeField] Transform launchTransform;

    public void Shoot()
    {
        npcWeapon.LaunchProjectile(launchTransform.position, FindObjectOfType<PlayerHealth>());
        
    }
}
