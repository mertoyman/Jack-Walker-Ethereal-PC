using UnityEngine;
using VRTK;

public delegate void SwordEventHandler(object sender);

public class SwordController : MonoBehaviour
{
    protected VRTK_ControllerEvents.ButtonAlias activationButton = VRTK_ControllerEvents.ButtonAlias.TriggerPress;

    public event SwordEventHandler SwordActivated;

    protected VRTK_ControllerEvents leftControllerEvents = null;
    protected VRTK_ControllerEvents rightControllerEvents = null;

    protected VRTK_InteractGrab leftControllerGrab = null;
    protected VRTK_InteractGrab rightControllerGrab = null;

    GrappleShoot grappleScript = null;
    Animator animator = null;

    [SerializeField] bool swordActivated;

    [SerializeField] protected bool leftButtonPressed;
    [SerializeField] protected bool rightButtonPressed;
    [SerializeField] protected bool leftControllerGrabbed;
    [SerializeField] protected bool rightControllerGrabbed;

    public virtual VRTK_ControllerEvents.ButtonAlias GetActivationButton()
    {
        return activationButton;
    }

    public virtual void SetActivationButton(VRTK_ControllerEvents.ButtonAlias button)
    {
        InitControllerListeners(false);
        activationButton = button;
        InitControllerListeners(true);
    }

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        grappleScript = FindObjectOfType<GrappleShoot>();
        InitListeners(true);
    }

    private void OnDisable()
    {
        InitListeners(false);
    }

    protected virtual void LeftButtonPressed(object sender, ControllerInteractionEventArgs e)
    {
        if (leftControllerGrabbed && this.transform.CompareTag("Sword"))
        {
            CheckForActivation();
        }
    }

    protected virtual void RightButtonPressed(object sender, ControllerInteractionEventArgs e)
    {
        if (rightControllerGrabbed && this.transform.CompareTag("Sword"))
        {
            CheckForActivation();
        }
    }

    private void RightButtonReleased(object sender, ControllerInteractionEventArgs e)
    {
        rightButtonPressed = false;
    }

    private void LeftButtonReleased(object sender, ControllerInteractionEventArgs e)
    {
        leftButtonPressed = false;
    }

    protected virtual void ObjectGrabbed(object sender, ObjectInteractEventArgs e)
    {


        //Left controller
        if (e.controllerReference.index == 0 && e.target.CompareTag("Sword"))
        {
            leftControllerGrabbed = true;
            rightControllerGrabbed = false;
            grappleScript.enabled = false;

        }
        //Right controller
        else if (e.controllerReference.index == 1 && e.target.CompareTag("Sword"))
        {
            leftControllerGrabbed = false;
            rightControllerGrabbed = true;
            grappleScript.enabled = true;
        }



    }
    private void ObjectUngrabbed(object sender, ObjectInteractEventArgs e)
    {
        if (e.controllerReference.index == 0 && e.target.CompareTag("Sword"))
        {
            leftControllerGrabbed = false;
            grappleScript.enabled = true;

        }
        //Right controller
        else if (e.controllerReference.index == 1 && e.target.CompareTag("Sword"))
        {
            rightControllerGrabbed = false;
        }


    }

    protected void OnSwordActivated()
    {
        if (SwordActivated != null)
        {
            SwordActivated(this);
        }
    }

    protected void InitListeners(bool state)
    {
        InitControllerListeners(state);
    }

    protected void InitControllerListeners(bool state)
    {
        InitControllerListener(state, VRTK_DeviceFinder.GetControllerLeftHand(), ref leftControllerEvents, ref leftControllerGrab, LeftButtonPressed, LeftButtonReleased); ;
        InitControllerListener(state, VRTK_DeviceFinder.GetControllerRightHand(), ref rightControllerEvents, ref rightControllerGrab, RightButtonPressed, RightButtonReleased);
    }

    protected void InitControllerListener(bool state, GameObject controller, ref VRTK_ControllerEvents events, ref VRTK_InteractGrab grab,
            ControllerInteractionEventHandler triggerPressed, ControllerInteractionEventHandler triggerReleased)
    {
        if (controller != null)
        {
            events = controller.GetComponentInChildren<VRTK_ControllerEvents>();
            grab = controller.GetComponentInChildren<VRTK_InteractGrab>();

            if (events != null)
            {
                if (state == true)
                {
                    events.SubscribeToButtonAliasEvent(activationButton, true, triggerPressed);
                    events.SubscribeToButtonAliasEvent(activationButton, false, triggerReleased);
                    grab.ControllerGrabInteractableObject += ObjectGrabbed;
                    grab.ControllerUngrabInteractableObject += ObjectUngrabbed;
                }
                else
                {
                    events.UnsubscribeToButtonAliasEvent(activationButton, true, triggerPressed);
                    events.SubscribeToButtonAliasEvent(activationButton, false, triggerReleased);
                    grab.ControllerGrabInteractableObject -= ObjectGrabbed;
                }
            }
        }
    }


    public virtual void CheckForActivation()
    {
        if (swordActivated)
        {
            animator.SetTrigger("Deactivate");
            swordActivated = false;
        }
        else
        {
            animator.SetTrigger("Activate");
            swordActivated = true;

        }

        OnSwordActivated();
    }

    public bool IsSwordActive()
    {
        return swordActivated;
    }
}