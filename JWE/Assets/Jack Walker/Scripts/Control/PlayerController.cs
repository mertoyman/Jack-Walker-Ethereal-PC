using UnityEngine;
using FPS.Resources;
using VRTK;
using FPS.Combat;

namespace FPS.Control
{
    public class PlayerController : MonoBehaviour
    {
        //    [SerializeField] Health health;  
        //    [SerializeField] VRTK_ControllerEvents leftController = null, rightController = null;
        //    [SerializeField] VRTK_InteractableObject weapon = null;
        //    [SerializeField] GrappleShoot grappleScript;
        //    [SerializeField] bool canShoot;

        //    private void OnEnable()
        //    {
        //        weapon.InteractableObjectGrabbed += WeaponPicked;
        //        weapon.InteractableObjectUngrabbed += WeaponDropped;
        //    }


        //    private void OnDisable()
        //    {
        //        weapon.InteractableObjectGrabbed -= WeaponPicked;
        //        weapon.InteractableObjectUngrabbed -= WeaponDropped;
        //    }

        //    private void WeaponPicked(object sender, InteractableObjectEventArgs e)
        //    {
        //        canShoot = true;
        //    }
        //    private void WeaponDropped(object sender, InteractableObjectEventArgs e)
        //    {
        //        canShoot = false;
        //    }

        //    void Start()
        //    {
        //        health = GetComponent<Health>();
        //    }


        //    void Update()
        //    {
        //        if (health.IsDead()) return;
        //        if (!InteractWithCombat()) return;
        //    }

        //    bool InteractWithCombat()
        //    {
        //        if(canShoot && (leftController.triggerPressed || rightController.triggerPressed))
        //        {
        //            if (!grappleScript.enabled)
        //            {
        //                //GetComponent<Fighter>().Shoot();
        //                return true;
        //            }
        //        }
        //        return false;
        //    }
        
        }
    }
