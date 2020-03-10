using UnityEngine;
using UnityEngine.AI;
using FPS.Movement;

public class PlayerEnemyCollision : MonoBehaviour
{
    NavMeshAgent agentScript;
    Mover moverScript;
    
    private void Start()
    {
        agentScript = this.GetComponentInParent<NavMeshAgent>();
        moverScript = GetComponentInParent<Mover>();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            agentScript.enabled = false;
            moverScript.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            agentScript.enabled = true;
            moverScript.enabled = true;
        }
    }
}
