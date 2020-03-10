using UnityEngine;
using VRTK;
using QFX.SFX;
using System;

public class LaserBeam : MonoBehaviour
{
    [SerializeField] GrappleShoot grappleScript;
    [SerializeField] VRTK_ControllerEvents leftController;
    [SerializeField] SFX_BeamWeapon beamWeapon;

    // Start is called before the first frame update
    private void OnEnable()
    {
        leftController.TriggerPressed += TriggerPressed;
        leftController.TriggerReleased += TriggerReleased;
    }

    private void OnDisable()
    {
        leftController.TriggerPressed -= TriggerPressed;
        leftController.TriggerReleased -= TriggerReleased;
    }

    private void TriggerPressed(object sender, ControllerInteractionEventArgs e)
    {
        if (grappleScript.grappleThrown)
        {
            Debug.Log("Hand thrown");
            beamWeapon.Run();
        }
    }

    private void TriggerReleased(object sender, ControllerInteractionEventArgs e)
    {
        if (grappleScript.grappleThrown)
        {
            beamWeapon.Stop();
        }
    }
}
