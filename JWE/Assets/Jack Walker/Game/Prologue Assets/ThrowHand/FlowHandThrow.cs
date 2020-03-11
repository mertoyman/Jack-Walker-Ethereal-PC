using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowHandThrow : MonoBehaviour
{
    [SerializeField] FlowManager flowManager;

    private void Update()
    {
        if(flowManager.flowPhase >= 5)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Hand"))
        {
            if (flowManager.flowPhase == 4)
            {
                flowManager.NextFlowLevel();
            }
            
        }
    }
}
