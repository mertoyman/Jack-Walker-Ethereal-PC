using FPS.Core;
using UnityEngine;
using UnityEngine.AI;

namespace FPS.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        [SerializeField] float maxSpeed = 6.0f;
        
        NavMeshAgent enemy;
        Animator animator;

        void Start()
        {
            animator = GetComponent<Animator>();
            enemy = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {
            
            if (animator == null) return;   
            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedFraction);
        }

        public void MoveTo(Vector3 destination, float speedFraction)
        {
            enemy.destination = destination;
            enemy.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            enemy.isStopped = false;
        }

        //Set blend tree speed to speed of the navmesh agent
        void UpdateAnimator()
        {
            Vector3 velocity = enemy.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            animator.SetFloat("forwardSpeed", speed);
        }

        public void Cancel()
        {
            enemy.isStopped = true;
        }

    }
}