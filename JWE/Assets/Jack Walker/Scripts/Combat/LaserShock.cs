using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShock : MonoBehaviour
{
    bool shocking = false;
    Animator targetAnimator = null;
    EnemyHealth target = null;
    GameObject meltTarget;
    [SerializeField] float laserDamage;
    //[SerializeField] Material meltMaterial;
    private void OnTriggerEnter(Collider other)
    {
        if (!target && other.gameObject.GetComponentInParent<EnemyHealth>())
        {
            target = other.gameObject.GetComponentInParent<EnemyHealth>();
            targetAnimator = target.gameObject.GetComponent<Animator>();
            if (!shocking)
            {
                StartCoroutine("GiveElectricity");
            }
        }
        else if (target != other.gameObject.GetComponentInParent<EnemyHealth>())
        {
            target = other.gameObject.GetComponentInParent<EnemyHealth>();

            if (target.gameObject.GetComponent<Animator>())
            {
                targetAnimator = target.gameObject.GetComponent<Animator>();
            }
            
            if (!shocking)
            {
                StartCoroutine("GiveElectricity");
            }

        }
        else if (!other.gameObject.GetComponentInParent<EnemyHealth>())
        {
            Debug.Log("Stopppppp");
            StopCoroutine("GiveElectricity");
            target = null;
            targetAnimator = null;
            shocking = false;
        }
        //if (other.gameObject.CompareTag("Meltable"))
        //{
        //    meltTarget = other.gameObject;
        //}
        //else
        //{
        //    StopCoroutine("Melt");
        //}
    }

    private IEnumerator GiveElectricity()
    {
        shocking = true;
        
        targetAnimator.SetTrigger("shock");
        target.TakeDamage(laserDamage);
        yield return new WaitForSeconds(1f);
        targetAnimator.SetTrigger("stun");
        target.TakeDamage(laserDamage * 2);
        yield return new WaitForSeconds(1.5f);
        target.TakeDamage(200);

    }

    //private IEnumerator Melt()
    //{
    //    meltTarget.GetComponent<Renderer>().material = meltMaterial;
    //    yield return new WaitForSeconds(1f);
    //    meltTarget.gameObject.SetActive(false);
    //}
}
