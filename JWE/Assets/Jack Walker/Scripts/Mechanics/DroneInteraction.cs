using VRTK;
using UnityEngine;

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

    private void OnDisable()
    {
        leftController.GripPressed -= GripPressed;
    }

    private void GripPressed(object sender, ControllerInteractionEventArgs e)
    {
        if (canExplode)
        {
            explosion.Play();
            Destroy(transform.gameObject, explosion.main.duration - 1);
            canExplode = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (grapple.grappleThrown && other.CompareTag("Hand"))
        {
            canExplode = true;
        }
    }
}
