using UnityEngine;
using FPS.Core;
using FPS.Control;
using FPS.Movement;

namespace FPS.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField]
        NPCWeaponShoot npcWeapon;

        [SerializeField]
        private float timeBetweenAttacks = 2f;
        private float timeSinceLastAttack = Mathf.Infinity;
        
        FieldOfView fov = null;
        Animator animator = null;
        Mover mover = null;
        
        [HideInInspector]
        public PlayerHealth target = null;

        private void Start()
        {
            animator = GetComponent<Animator>();
            mover = GetComponent<Mover>();
            target = FindObjectOfType<PlayerHealth>();
            fov = GetComponentInChildren<FieldOfView>();
        }

        public void Attack()
        {
            timeSinceLastAttack += Time.deltaTime;
            transform.LookAt(target.transform);
            if(timeSinceLastAttack > timeBetweenAttacks)
            {
                animator.ResetTrigger("stopAttack");
                animator.SetTrigger("attack");
                timeSinceLastAttack = 0;
                npcWeapon.Shoot();
            }   
        }

        public void Cancel()
        {
            animator.ResetTrigger("attack");
            animator.SetTrigger("stopAttack");
        }

        public bool IsTargetInRange()
        {
            return fov.IsPlayerDetected();
        }
    }
}