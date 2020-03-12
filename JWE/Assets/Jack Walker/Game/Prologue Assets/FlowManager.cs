using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class FlowManager : MonoBehaviour
{
    [SerializeField] GameObject[] flowChart;
    [SerializeField] GameObject parentGlass;
    [SerializeField] GameObject leftHand;
    [SerializeField] GrappleShoot gShoot;
    [SerializeField] VRTK_Pointer pointer;
    [SerializeField] GameObject sword, doorTrigger;
    public int flowPhase = 0;
    public int melted = 0;

    private void Start()
    {
        doorTrigger.SetActive(false);
        leftHand.SetActive(false);
        FindObjectOfType<LaserBeam>().enabled = true;
    }
    public void NextFlowLevel()
    {
        if (flowPhase == 1)
        {
            leftHand.SetActive(true);
            FindObjectOfType<LaserBeam>().enabled = false;
            gShoot.grappleThrown = false;
            gShoot.enabled = false;
            Destroy(flowChart[flowPhase].transform.parent.gameObject);
        }
        else if (flowPhase == 3)
        {
            doorTrigger.SetActive(true);
        }

        flowChart[flowPhase].SetActive(false);
        flowPhase++;
        flowChart[flowPhase].SetActive(true);

    }

    private void Update()
    {
        if (flowPhase == 2 && pointer.isLive)
        {
            gShoot.enabled = true;
            sword.SetActive(false);
            sword.SetActive(true);
            NextFlowLevel();
        }
        if (flowPhase == 3 && sword.transform.parent)
        {
            if (sword.transform.parent.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                NextFlowLevel();
            }
        }
    }
    public void MeltedDone()
    {
        melted++;
        if (melted >= 45 && flowPhase == 0)
        {
            parentGlass.GetComponent<Animator>().SetTrigger("Drop");
            NextFlowLevel();
        }
    }
}
