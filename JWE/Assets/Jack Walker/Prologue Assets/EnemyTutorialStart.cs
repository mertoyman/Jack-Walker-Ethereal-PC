using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTutorialStart : MonoBehaviour
{
    [SerializeField] FlowManager flowManager;
    [SerializeField] GameObject doorTrigger;
    private void Start()
    {
        doorTrigger.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player") && flowManager.flowPhase == 5) 
        {
            flowManager.NextFlowLevel();
            doorTrigger.SetActive(true);
        }
    }
}
