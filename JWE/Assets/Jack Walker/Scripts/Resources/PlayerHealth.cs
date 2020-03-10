using FPS.Resources;
using UnityEngine;
using VRTK;

public class PlayerHealth : Health
{
    [SerializeField] VRTK_BasicTeleport teleportAfterDeath;
    [SerializeField] Transform checkpoint;

    public override void Die()
    {
        Debug.Log("You died");
        teleportAfterDeath.ForceTeleport(checkpoint.position);
    }
}
