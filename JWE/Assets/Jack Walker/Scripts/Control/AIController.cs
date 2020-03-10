using System.Collections;
using UnityEngine;
using FPS.Movement;
using FPS.Combat;

namespace FPS.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float waypointDwellTime = 3f;
        [SerializeField] float waypointTolerance = 1;
        [SerializeField] float chaseDelay = 5f;
        [SerializeField] [Range(0, 1)] float patrolSpeedFraction = 0.2f;
        [SerializeField] [Range(0, 1)] float chaseSpeedFraction = 0.6f;
        [SerializeField] PatrolPath patrolPath = null;
        [SerializeField] bool alerted = false;
        
        Fighter fighter = null;
        Mover mover;

        Vector3 guardPosition;
        int currentWaypointIndex = 0;
        //float timeSinceLastSawPlayer = Mathf.Infinity;
        float timeSinceArrivedAtWaypoint = Mathf.Infinity;

        void Start()
        {
            fighter = GetComponent<Fighter>();
            mover = GetComponent<Mover>();           
            guardPosition = transform.position;
        }

        void Update()
        {

            if (fighter.IsTargetInRange() && !fighter.target.IsDead())
            {
                AttackBehaviour();
            }
            else if (!fighter.IsTargetInRange() && alerted)
            {
                Debug.Log("Chasing target");
                //transform.LookAt(fighter.target.transform);
                transform.rotation.SetFromToRotation(transform.position, fighter.target.transform.position);
                StartCoroutine("ChasePlayerWithDelay", chaseDelay);
            
            }
            else
            {
                PatrolBehaviour();
            }

            UpdateTime();
        }

        private void UpdateTime()
        {
            //Used to calculate the dwelling time of NPC whilst patrolling.
            timeSinceArrivedAtWaypoint += Time.deltaTime;
        }

        private void AttackBehaviour()
        {
            Debug.Log("Attacking");
            alerted = true;
            fighter.Attack();
            mover.Cancel();

        }


        void PatrolBehaviour()
        {
            Vector3 nextPosition = guardPosition;

            if (patrolPath != null)
            {
                if (AtWaypoint())
                {
                    timeSinceArrivedAtWaypoint = 0;
                    CycleWaypoint();
                }

                nextPosition = GetCurrentWaypoint();
            }

            if (timeSinceArrivedAtWaypoint > waypointDwellTime)
            {
                mover.StartMoveAction(nextPosition, patrolSpeedFraction);
            }
        }

        IEnumerator ChasePlayerWithDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            mover.StartMoveAction(fighter.target.transform.position, chaseSpeedFraction);
            fighter.Cancel();
            if (Vector3.Distance(fighter.target.transform.position, transform.position) <= 10f)
            {
                mover.Cancel();
            }
        }

        //Which waypoint is the AI currently positioned on.
        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

        //Cycle through each waypoint index.
        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        //Check if the AI is on the patrol path
        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
        }

        //void AlertEveryOne()
        //{
        //    var allNpcsOnScene = FindObjectsOfType<AIController>();

        //    foreach(var npc in allNpcsOnScene)
        //    {
        //        npc.alerted = true;
        //    }
        //}
    }
}