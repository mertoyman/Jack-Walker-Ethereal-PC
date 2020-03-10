using VRTK;
using UnityEngine;
using System;

public class DroneInteraction : MonoBehaviour
{
    [SerializeField] ParticleSystem explosion = null;
    [SerializeField] GrappleShoot grapple;
    [SerializeField] VRTK_ControllerEvents leftController;

    [SerializeField] bool canExplode;

    private void OnEnable()
    {
        leftController.GripPressed += GripPressed; 
    }

    private void GripPressed(object sender, ControllerInteractionEventArgs e)
    {
        if (canExplode)
        {
            explosion.Play();
            Destroy(transform.gameObject, explosion.main.duration - 1);
            grapple.PullBackGrapple();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Gauntlet>() && !grapple.canMove)
        {
            canExplode = true;
        }
    }
}
