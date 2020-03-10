using UnityEngine;
using VRTK;

public class StealthKill : MonoBehaviour
{
    VRTK_ControllerEvents controllerEvents = null;
    GameObject leftHand = null;
    Animator animator = null;

    [SerializeField] bool stealthKill = false;

    private void OnEnable()
    {
        controllerEvents = FindObjectOfType<GrappleShoot>().GetComponent<VRTK_ControllerEvents>();
        animator = GetComponentInParent<Animator>();
        leftHand = FindObjectOfType<Gauntlet>().gameObject;
        controllerEvents.GripPressed += GripPressed;
    }

    private void GripPressed(object sender, ControllerInteractionEventArgs e)
    {
        if (stealthKill)
        {
            animator.SetTrigger("stealthKill");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == leftHand)
        {
            stealthKill = true;
        }
    }
}
