using VRTK;
using VRTK.GrabAttachMechanics;
using VRTK.SecondaryControllerGrabActions;
using UnityEngine;

public class AssignMissingComponents : MonoBehaviour
{
    VRTK_InteractableObject[] interactableObject = null;
    VRTK_ClimbableGrabAttach[] climbableGrabAttach = null;
    

    void Awake()
    {
        interactableObject = this.gameObject.GetComponentsInChildren<VRTK_InteractableObject>();

        for (int i = 0; i < interactableObject.Length; i++)
        {
            interactableObject[i].grabAttachMechanicScript = this.gameObject.GetComponent<VRTK_BaseGrabAttach>();
            interactableObject[i].secondaryGrabActionScript = this.gameObject.GetComponent<VRTK_BaseGrabAction>();
            interactableObject[i].isGrabbable = true;
            interactableObject[i].stayGrabbedOnTeleport = false;
            interactableObject[i].holdButtonToUse = false;

        }

        climbableGrabAttach = this.gameObject.GetComponentsInChildren<VRTK_ClimbableGrabAttach>();
        
        for (int i = 0; i < climbableGrabAttach.Length; i++)
        {
            climbableGrabAttach[i].precisionGrab = true;
            //Destroy(GetComponent(typeof(AssignMissingComponents)));
        }
    }
}
