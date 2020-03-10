using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCut : MonoBehaviour
{
    [SerializeField] GameObject cutPlate = null, cutScratch = null, spark = null;
    GameObject sparkFollow;
    [SerializeField]LineRenderer lineRenderer = null, lineRendererCold = null;
    Vector3 startPos, endPos;
    Vector3 positions = Vector3.zero;
    int posCounter;
    GameObject wallCutCold, wallCutHot, sparkObj;
    private void Start()
    {
        sparkFollow = (GameObject)Instantiate(spark, transform.position, Quaternion.identity);
        sparkFollow.SetActive(false);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Wall") || other.collider.CompareTag("Floor"))
        {
            startPos = other.GetContact(0).point;

            posCounter = 1;

            sparkObj = (GameObject)Instantiate(spark, startPos - new Vector3(0, 0, 0.0001f), Quaternion.identity);
            Destroy(sparkObj, 0.1f);

            wallCutHot = (GameObject)Instantiate(cutPlate, transform.position, Quaternion.identity);
            lineRenderer = wallCutHot.GetComponent<LineRenderer>();
            lineRenderer.positionCount = posCounter;
            lineRenderer.SetPosition(posCounter - 1, startPos);

            wallCutCold = (GameObject)Instantiate(cutScratch, transform.position, Quaternion.identity);
            lineRendererCold = wallCutCold.GetComponent<LineRenderer>();
            lineRendererCold.positionCount = posCounter;
            lineRendererCold.SetPosition(posCounter - 1, startPos);
        }
    }

    private void OnCollisionStay(Collision other)
    {
        positions = other.contacts[other.contacts.Length - 1].point;
        StartCut();
    }

    private void OnCollisionExit(Collision other)
    {
        endPos = positions;
        posCounter++;

        sparkFollow.SetActive(false);


        lineRenderer.positionCount = posCounter;
        lineRenderer.SetPosition(posCounter - 1, endPos);


        lineRendererCold.positionCount = posCounter;
        lineRendererCold.SetPosition(posCounter - 1, endPos);

        for (int i = 0; i < posCounter / 2; i++)
        {
            Vector3 temp = lineRenderer.GetPosition(i);
            lineRenderer.SetPosition(i, lineRenderer.GetPosition(posCounter - 1 - i));
            lineRenderer.SetPosition((posCounter - 1 - i), temp);

            Vector3 tempCold = lineRendererCold.GetPosition(i);
            lineRendererCold.SetPosition(i, lineRendererCold.GetPosition(posCounter - 1 - i));
            lineRendererCold.SetPosition((posCounter - 1 - i), temp);
        }

        wallCutHot.GetComponent<DeleteScratch>().enabled = true;
        wallCutCold.GetComponent<DeleteScratch>().enabled = true;
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Mouse0))
    //    {
    //        RaycastHit hit;
    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        if (Physics.Raycast(ray, out hit))
    //            if (hit.collider != null)
    //            {
                    
    //            }

    //    }
    //    if (Input.GetKey(KeyCode.Mouse0))
    //    {

            

    //    }
    //    if (Input.GetKeyUp(KeyCode.Mouse0))
    //    {
            
    //        RaycastHit hit;
    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        if (Physics.Raycast(ray, out hit))
    //            if (hit.collider != null)
    //            {
                    


    //            }

            

    //    }
    //}

    void StartCut()
    {
                
        //if (Vector3.Distance(lineRenderer.GetPosition(posCounter-1), positions) > 0.5f)
          //      {
                    posCounter++;
                    if (sparkFollow.activeInHierarchy == false)
                    {
                        sparkFollow.SetActive(true);
                        sparkFollow.transform.position = positions - new Vector3(0, 0, 0.0001f);
                    }
                    else
                    {
                        sparkFollow.transform.position = positions - new Vector3(0, 0, 0.0001f);
                    }

                    lineRenderer.positionCount = posCounter;
                    lineRenderer.SetPosition(posCounter - 1, positions);
                    lineRendererCold.positionCount = posCounter;
                    lineRendererCold.SetPosition(posCounter - 1, positions);
            //    }
                    
    }
}
