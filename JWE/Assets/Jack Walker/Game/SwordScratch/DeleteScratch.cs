using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteScratch : MonoBehaviour
{
    LineRenderer lineRenderer;
    [SerializeField] float deleteTimer;
    float activeTimer;
    Color newColor;
    float newAlpha = 1f;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        activeTimer = deleteTimer;
        
    }


    void Update()
    {
        if (activeTimer > 0)
        {
            activeTimer -= Time.deltaTime;
        }
        else
        {

            DeletePosition();
            activeTimer = deleteTimer / 10;
            //activeTimer = deleteTimer;

        }
    }

    void DeletePosition()
    {

        if (lineRenderer.positionCount > 2)
        {
            lineRenderer.positionCount = lineRenderer.positionCount - 1;

        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
