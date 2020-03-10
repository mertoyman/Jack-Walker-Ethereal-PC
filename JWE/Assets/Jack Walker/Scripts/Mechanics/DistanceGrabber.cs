using System;
using UnityEngine;
using VRTK;
using VRTK.GrabAttachMechanics;

public class DistanceGrabber : MonoBehaviour
{
    [SerializeField] VRTK_ChildOfControllerGrabAttach grabber = null;
    [SerializeField] VRTK_InteractGrab interactGrab = null;
    [SerializeField] VRTK_InteractTouch interactTouch = null;
    [SerializeField] VRTK_Pointer pointer = null;
    [SerializeField] GrappleShoot grappleScript = null;

    public bool grabbed;
    public bool touched;
    public bool detected;
    public int grabCount = 0;
    public bool canBeGrabbed;

    
    private void OnEnable()
    {
        interactGrab.GrabButtonPressed += GrabButtonPressed;
        pointer.DestinationMarkerEnter += InteractableObjectDetected;
        pointer.DestinationMarkerExit += InteractableObjectUndetected;
        interactTouch.ControllerTouchInteractableObject += InteractableObjectTouched;
        interactTouch.ControllerUntouchInteractableObject += InteractableObjectUntouched;
    }

    private void InteractableObjectUntouched(object sender, ObjectInteractEventArgs e)
    {
        touched = false;
    }

    private void InteractableObjectTouched(object sender, ObjectInteractEventArgs e)
    {
        touched = true;
    }

    private void Update()
    {
        if(grabCount == 0 && grabbed)
        {
            var target = GetTarget();
            target.transform.parent = null;
            grappleScript.enabled = true;
            grabbed = false;
        }     
        
        if(touched && detected)
        {
            canBeGrabbed = false;
        }

    }

    private void InteractableObjectUndetected(object sender, DestinationMarkerEventArgs e)
    {
        detected = false;
        canBeGrabbed = false;
        if(!grabbed)
            SetTarget(null);

        //Deactivate outline of the weapon
        
    }

    private void InteractableObjectDetected(object sender, DestinationMarkerEventArgs e)
    {
        if (e.raycastHit.collider.GetComponent<VRTK_InteractableObject>())
        {
            canBeGrabbed = true;
            detected = true;
            SetTarget(e.raycastHit.collider.GetComponent<VRTK_ChildOfControllerGrabAttach>());

            //Activate outline of the weapon
        }
    }

    private void GrabButtonPressed(object sender, ControllerInteractionEventArgs e)
    {
        if (!grabbed && canBeGrabbed)
        {
            var target = GetTarget();
            target.StartGrab(this.gameObject, target.gameObject, this.gameObject.GetComponent<Rigidbody>());

            grabbed = true;
            detected = false;
            grabCount++;
            grappleScript.enabled = false;
        }
        else
        {
            grabCount = 0;
        }

    }

    private void SetTarget(VRTK_ChildOfControllerGrabAttach target)
    {
        grabber = target;
    }

    private VRTK_ChildOfControllerGrabAttach GetTarget()
    {
        return grabber;
    }
}
