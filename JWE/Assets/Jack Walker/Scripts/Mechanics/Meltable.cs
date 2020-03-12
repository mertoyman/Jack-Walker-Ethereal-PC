using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meltable : MonoBehaviour
{

    [SerializeField]FlowManager flowManager;
    Material meltMat;
    bool meltStarted = false;
    float meltRatio;
    private void Start()
    {
        meltMat = GetComponent<Renderer>().material;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LaserEnd"))
        {
            StartCoroutine("Melt");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("LaserEnd") && !meltStarted)
        {
            StopCoroutine("Melt");
        }
    }


    private IEnumerator Melt()
    {
        yield return new WaitForSeconds(0.2f);
        meltStarted = true;
        while (meltMat.GetFloat("_MeltRatio") < 1)
        {
            meltRatio = meltMat.GetFloat("_MeltRatio");
            meltMat.SetFloat("_MeltRatio",meltRatio+0.02f );
            yield return new WaitForEndOfFrame();

        }
        flowManager.MeltedDone();
        Destroy(this.gameObject);

    }
}
