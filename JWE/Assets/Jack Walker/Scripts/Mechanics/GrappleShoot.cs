using UnityEngine;
using VRTK;

public class GrappleShoot : MonoBehaviour
{
    [SerializeField] VRTK_Pointer pointer = null;
    [SerializeField] VRTK_InteractGrab grab = null;
    [SerializeField] VRTK_ControllerEvents controllerEvents = null;
    [SerializeField] Gauntlet gauntlet;
    [SerializeField] Transform player = null;
    //[SerializeField] FlowManager flowManager;
    [SerializeField] Vector3 initialGrapplePosition = new Vector3(-0.032f, -0.022f, -0.11f);
    
    [SerializeField] bool triggerPress;
    [SerializeField] bool YPressed;
    [SerializeField] bool grabPress;
    [SerializeField] bool isFlying;
    [SerializeField] bool canGrab = false;
    [SerializeField] float flySpeed = 2;
    
    VRTK_BodyPhysics body = null;
    SlowMotion slowMotion;
    GameObject controller = null;
    LaserBeam laserBeamScript;
    Jump jump = null;

    public Vector3 target;
    
    public float projectileSpeed = 20f;
    public bool grappleThrown;
    public bool isHit;
    public bool canMove;
    public bool midAirPull;
    public bool isGrappleBack;

    private void Start()
    {
        //flowManager = FindObjectOfType<FlowManager>();
        body = FindObjectOfType<VRTK_BodyPhysics>();
        jump = FindObjectOfType<Jump>();
        slowMotion = FindObjectOfType<SlowMotion>();
        controller = FindObjectOfType<VRTK_SlideObjectControlAction>().gameObject;
        laserBeamScript = GetComponent<LaserBeam>();
        initialGrapplePosition = new Vector3(-0.032f, -0.022f, -0.175f);
        grappleThrown = true;
    }

    private void OnEnable()
    {
        controllerEvents.ButtonTwoPressed += ButtonYPressed;
        controllerEvents.TriggerReleased += TriggerReleased;
        controllerEvents.TriggerPressed += TriggerPressed;
        pointer.DestinationMarkerEnter += DestinationMarkerEnter;
        pointer.DestinationMarkerHover += DestinationMarkerHover;
        pointer.DestinationMarkerExit += DestinationMarkerExit;
        grab.GrabButtonPressed += GrabButtonPressed;

    }

    private void OnDisable()
    {
        controllerEvents.TriggerReleased -= TriggerReleased;
        controllerEvents.TriggerPressed -= TriggerPressed;
        pointer.DestinationMarkerEnter -= DestinationMarkerEnter;
        pointer.DestinationMarkerHover -= DestinationMarkerHover;
        pointer.DestinationMarkerExit -= DestinationMarkerExit;
        grab.GrabButtonPressed -= GrabButtonPressed;
    }

    private void FixedUpdate()
    {
        if (triggerPress && !canGrab && isHit)
        {
            isGrappleBack = false;
            FireGrapple();
        }

        if (grabPress && canGrab)
        {
            isGrappleBack = true;
            PullBackGrapple();

        }

        if (YPressed && canMove)
        {
            MoveToGrapple();
        }

        if (grabPress && isFlying)
        {
            midAirPull = true;
            PullBackGrapple();
        }
    }
    private void ButtonYPressed(object sender, ControllerInteractionEventArgs e)
    {
        if (!triggerPress && isHit)
            YPressed = true;
    }

    private void GrabButtonPressed(object sender, ControllerInteractionEventArgs e)
    {
        grabPress = true;
        triggerPress = false;
        isHit = false;
        //if (flowManager.flowPhase == 1)
        //{
        //    flowManager.NextFlowLevel();
        //}
    }

    private void TriggerPressed(object sender, ControllerInteractionEventArgs e)
    {
        triggerPress = true;
        grabPress = false;
    }
    private void TriggerReleased(object sender, ControllerInteractionEventArgs e)
    {
        isHit = false;
        triggerPress = false;
    }

    private void DestinationMarkerEnter(object sender, DestinationMarkerEventArgs e)
    {
        if (e.raycastHit.collider.gameObject.layer != LayerMask.NameToLayer("Pistol") || e.raycastHit.collider.gameObject.layer != LayerMask.NameToLayer("Sword"))
        {
            isHit = e.raycastHit.collider;
            target = e.raycastHit.point;
            Debug.Log(e.raycastHit.collider.name);
        }
    }

    private void DestinationMarkerHover(object sender, DestinationMarkerEventArgs e)
    {
        if (e.raycastHit.collider.gameObject.layer != LayerMask.NameToLayer("Pistol") || e.raycastHit.collider.gameObject.layer != LayerMask.NameToLayer("Sword"))
        {
            isHit = e.raycastHit.collider;
            target = e.raycastHit.point;
        }

    }

    private void DestinationMarkerExit(object sender, DestinationMarkerEventArgs e)
    {
        if (e.raycastHit.collider.gameObject.layer != LayerMask.NameToLayer("Pistol") || e.raycastHit.collider.gameObject.layer != LayerMask.NameToLayer("Sword"))
        {
            isHit = e.raycastHit.collider;
            target = e.raycastHit.point;
        }

    }


    private void FireGrapple()
    {
        projectileSpeed = 20;
        gauntlet.enabled = true;
        canMove = false;
        canGrab = true;
        grappleThrown = true;
        laserBeamScript.enabled = true;
        pointer.enabled = false;
        //if (flowManager.flowPhase == 4)
        //{
        //    flowManager.NextFlowLevel();
        //}
    }

    private void MoveToGrapple()
    {
        gauntlet.enabled = false;
        laserBeamScript.enabled = false;
        isHit = false;
        isFlying = true;

        if (!grabPress)
        {
            pointer.enabled = false;
            //body.ToggleOnGround(false);
            player.GetComponent<Rigidbody>().useGravity = false;

            var gauntletPosition = new Vector3(gauntlet.transform.position.x - transform.position.x + player.position.x,
                                               gauntlet.transform.position.y - transform.position.y + player.position.y,
                                               gauntlet.transform.position.z - transform.position.z + player.position.z);

            player.position = Vector3.MoveTowards(player.position, gauntletPosition, flySpeed * Time.deltaTime * slowMotion.slowMotionMultiplier);

            EnableMovementScript(false);
        }
        else
        {
            player.GetComponent<Rigidbody>().isKinematic = false;
            //body.ToggleOnGround(true);
            YPressed = false;
            EnableMovementScript(true);

        }

        if (Vector3.Distance(player.position, gauntlet.transform.position) < 0.5f)
        {
            canMove = false;
            YPressed = false;
            //body.ToggleOnGround(true);
            jump.enabled = false;
            player.GetComponent<Rigidbody>().isKinematic = true;
            gauntlet.transform.parent = transform;

        }
    }

    public void PullBackGrapple()
    {
        pointer.enabled = true;
        gauntlet.enabled = false;
        projectileSpeed = 20;
        laserBeamScript.enabled = false;
        canGrab = false;
        isHit = false;
        gauntlet.transform.position = Vector3.Lerp(gauntlet.transform.position, transform.position, projectileSpeed /** Vector3.Distance(grapple.position, transform.position)*/);
        if (Vector3.Distance(pointer.transform.position, gauntlet.transform.position) < 0.5f)
        {
            player.GetComponent<Rigidbody>().useGravity = true;
            gauntlet.transform.parent = transform;

            gauntlet.transform.localPosition = initialGrapplePosition;
            gauntlet.transform.localRotation = Quaternion.Euler(-90, -90, 180);
            jump.enabled = true;
            grappleThrown = false;
            midAirPull = false;
        }
    }

    private void EnableMovementScript(bool enabled)
    {
        var movementScripts = controller.GetComponents<VRTK_SlideObjectControlAction>();
        for (int i = 0; i < movementScripts.Length; i++)
        {
            movementScripts[i].enabled = enabled;
        }
    }
}
