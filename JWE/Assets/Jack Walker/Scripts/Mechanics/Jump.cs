using UnityEngine;
using VRTK;

public class Jump : VRTK_SlingshotJump
{
    protected bool rightButtonPressed;

    protected override void RightButtonPressed(object sender, ControllerInteractionEventArgs e)
    {
        // Check for new right aim
        if (!rightIsAiming && !IsClimbing())
        {
            rightIsAiming = true;
            rightStartAimPosition = playArea.InverseTransformPoint(rightControllerEvents.gameObject.transform.position);
            rightButtonPressed = true;
        }

        CheckForReset();
        CheckForJump();
    }

    protected override void CheckForJump()
    {
        if (rightButtonPressed && bodyPhysics.OnGround())
        {
            Vector3 worldJumpDir = playArea.transform.TransformVector(Vector3.up);
            Vector3 jumpVector = worldJumpDir * velocityMultiplier;

            if (jumpVector.magnitude > velocityMax)
            {
                jumpVector = jumpVector.normalized * velocityMax;
            }

            bodyPhysics.ApplyBodyVelocity(jumpVector, true, true);

            UnAim();

            OnSlingshotJumped();
        }
    }

    protected override void LeftButtonReleased(object sender, ControllerInteractionEventArgs e)
    {
        
    }
}
