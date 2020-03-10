using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Holsterable : MonoBehaviour
{
    [SerializeField] bool holstered = false;
    [SerializeField] GameObject targetHolster = null;
    [SerializeField] VRTK_InteractableObject interactableObject;


    public void SetTargetHolster(GameObject _targetHolster)
    {
        targetHolster = _targetHolster;
    }

    public GameObject GetTargetHolster()
    {
        return targetHolster;
    }


    private void OnEnable()
    {
        interactableObject = GetComponent<VRTK_InteractableObject>();
        interactableObject.InteractableObjectUngrabbed += WeaponDropped;

    }

    public void WeaponDropped(object sender, InteractableObjectEventArgs e)
    {
        if (targetHolster && targetHolster.GetComponent<Holster>().ownedWeapon == this.gameObject)
        {
            this.transform.parent = targetHolster.transform;
            if (this.CompareTag("Pistol"))
            {
                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.Euler(90, 0, 0);
            }
            else
            {
                transform.localPosition = new Vector3(0, -2.5f, -1);
                transform.localRotation = Quaternion.Euler(180, 0, -90);
                
            }
        }
        else
        {
            SetTargetHolster(null);
        }
       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == targetHolster)
        {
            holstered = false;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!holstered && other.gameObject.GetComponent<Holster>())
        {
            other.gameObject.GetComponent<Holster>().ownedWeapon = this.gameObject;

            holstered = true;
            Debug.Log("Holstered");

            if (GetComponent<SwordController>().IsSwordActive() && GetComponent<SwordController>())
            {
                GetComponent<SwordController>().CheckForActivation();
            }
        }
    }


}
