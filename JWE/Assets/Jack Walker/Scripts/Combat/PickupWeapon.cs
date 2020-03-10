using VRTK;
using UnityEngine;
//using FORGE3D;

namespace FPS.Combat
{
    public class PickupWeapon : MonoBehaviour
    {       
        public bool canShoot;
        [SerializeField] VRTK_InteractableObject linkedObject = null;
        //[SerializeField] GameObject projectilePrefab = null;
        //[SerializeField] Transform projectileSpawnPosition = null;
        //[SerializeField] Weapon weapon;
        [SerializeField] GrappleShoot grappleScript = null;
        //[SerializeField] F3DFXController fxController;
        //[SerializeField] float projectileSpeed = 1000;

        //RaycastHit hit;

        protected virtual void OnEnable()
        {
            linkedObject = (linkedObject == null ? GetComponent<VRTK_InteractableObject>() : linkedObject);

            if (linkedObject != null)
            {
                linkedObject.InteractableObjectUsed += InteractableObjectUsed;
                linkedObject.InteractableObjectGrabbed += InteractableObjectGrabbed;
                linkedObject.InteractableObjectUngrabbed += InteractableObjectUngrabbed;
            }
        }

        private void InteractableObjectUngrabbed(object sender, InteractableObjectEventArgs e)
        {
            if (e.interactingObject == grappleScript.gameObject)
            {
                grappleScript.enabled = true;
            }
        }

        private void InteractableObjectGrabbed(object sender, InteractableObjectEventArgs e)
        {
            if(e.interactingObject == grappleScript.gameObject)
            {
                grappleScript.enabled = false;
            }
        }

        protected virtual void OnDisable()
        {
            if (linkedObject != null)
            {
                linkedObject.InteractableObjectUsed -= InteractableObjectUsed;
                linkedObject.InteractableObjectGrabbed -= InteractableObjectGrabbed;
                linkedObject.InteractableObjectUngrabbed -= InteractableObjectUngrabbed;
            }
        }
        
        public virtual void InteractableObjectUsed(object sender, InteractableObjectEventArgs e)
        {
            //weapon.LaunchProjectile(projectileSpawnPosition.position, transform.forward);
            //fxController.Fire();
        }
    }
}