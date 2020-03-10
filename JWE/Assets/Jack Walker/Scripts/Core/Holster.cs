using UnityEngine;
using VRTK;

public class Holster : MonoBehaviour
{

    public GameObject ownedWeapon = null;

    [SerializeField] VRTK_InteractGrab leftInteractGrab;
    [SerializeField] VRTK_InteractGrab rightInteractGrab;

    private void OnEnable()
    {

        leftInteractGrab.ControllerUngrabInteractableObject += WeaponDropped;
        rightInteractGrab.ControllerUngrabInteractableObject += WeaponDropped;
    }
    private void WeaponDropped(object sender, ObjectInteractEventArgs e)
    {
        if (e.target.GetComponent<Holsterable>())
        {
            if (e.target.GetComponent<Holsterable>().GetTargetHolster() == null && !ownedWeapon)
            {
                ownedWeapon = e.target;
                e.target.GetComponent<Holsterable>().SetTargetHolster(this.gameObject);

                e.target.transform.parent = this.transform;
                if (e.target.CompareTag("Pistol"))
                {
                    e.target.transform.localPosition = Vector3.zero;
                    e.target.transform.localRotation = Quaternion.Euler(90, 0, 0);
                }
                else
                {
                    e.target.transform.localPosition = new Vector3(0, -2.5f, -1);
                    e.target.transform.localRotation = Quaternion.Euler(180, 0, -90);
                }
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != ownedWeapon && !this.gameObject.GetComponentInChildren<Holsterable>())
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Pistol") || other.gameObject.layer == LayerMask.NameToLayer("Sword"))
            {

                ownedWeapon = other.gameObject;
                if (other.GetComponent<Holsterable>() != null)
                {
                   if(other.GetComponent<Holsterable>().GetTargetHolster() != null) 
                        other.GetComponent<Holsterable>().GetTargetHolster().GetComponent<Holster>().ownedWeapon = null;
                    
                    other.GetComponent<Holsterable>().SetTargetHolster(this.gameObject);
                }

            }

        }
    }
    
}
