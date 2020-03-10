using UnityEngine;

public class Gauntlet : MonoBehaviour
{
    [SerializeField] GrappleShoot grappleScript;
   
    private void Update()
    {
        ThrowHand();
    }

    void ThrowHand()
    {
        transform.position = Vector3.MoveTowards(transform.position, grappleScript.target, grappleScript.projectileSpeed * Time.deltaTime);
        transform.parent = null;

        if (Vector3.Distance(transform.position, grappleScript.target) < 0.001f)
        {
            grappleScript.canMove = true;
            grappleScript.isHit = true;
        }
    }
}
