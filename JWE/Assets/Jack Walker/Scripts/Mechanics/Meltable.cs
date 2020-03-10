using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meltable : MonoBehaviour
{

    [SerializeField]FlowManager flowManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LaserEnd"))
        {
            StartCoroutine("Melt");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("LaserEnd"))
        {
            StopCoroutine("Melt");
        }
    }


    private IEnumerator Melt()
    {
        yield return new WaitForSeconds(0.4f);
        flowManager.MeltedDone();
        Destroy(this.gameObject);

    }
}
