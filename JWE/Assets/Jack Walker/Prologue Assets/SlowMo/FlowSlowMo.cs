using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
public class FlowSlowMo : MonoBehaviour
{
    [SerializeField] FlowManager _flowManager;
    [SerializeField] VRTK_ControllerEvents rightController = null;
    bool gameStop = false;
    private void OnEnable()
    {
        rightController.ButtonTwoPressed += Button_B_Pressed;
    }
    private void Button_B_Pressed(object sender, ControllerInteractionEventArgs e)
    {
        if (gameStop)
        {
            gameStop = false;
            _flowManager.NextFlowLevel();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && !gameStop)
        {
            if (_flowManager.flowPhase == 6)
            {
                _flowManager.NextFlowLevel();
                Time.timeScale = 0.05f;
                gameStop = true;
            }
        }
    }


}
