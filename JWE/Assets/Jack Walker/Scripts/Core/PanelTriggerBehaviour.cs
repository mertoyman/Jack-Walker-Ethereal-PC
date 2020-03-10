using UnityEngine;
using UnityEngine.Events;

public class PanelTriggerBehaviour : MonoBehaviour
{
    public UnityEvent onPanelTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Door");
            onPanelTrigger.Invoke();
        }
    }
}
