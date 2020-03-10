using FPS.Core;
using FPS.Combat;
using FPS.Resources;
using FPS.Control;
using UnityEngine;

public class EnemyHealth : Health
{
    [SerializeField] Animator animator = null;
    [SerializeField] Fighter fighter = null;
    [SerializeField] AIController npcController = null;
   
    void Start()
    {
        animator = this.GetComponent<Animator>();
        fighter = GetComponent<Fighter>();
        npcController = GetComponent<AIController>();
    }
    
    public override void Die()
    {
        animator.SetTrigger("die");
        GetComponent<ActionScheduler>().CancelCurrentAction();
    }
    
    //Animation event
    public void DisableComponentsInDeath()
    {
        animator.enabled = false;
        npcController.enabled = false;
        fighter.enabled = false;
    }
}