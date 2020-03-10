using System;
using System.Collections;
using UnityEngine;
using VRTK;

public class SlowMotion : MonoBehaviour
{
    [SerializeField] VRTK_ControllerEvents rightController = null;
    public float slowMotionMultiplier = 1;
    bool isSlowMotion = false;

    private void OnEnable()
    {
        rightController.ButtonTwoPressed += Button_B_Pressed;    
    }

    private void Button_B_Pressed(object sender, ControllerInteractionEventArgs e)
    {
        DoSlowMotion();
    }

    public void DoSlowMotion()
    {
        if (isSlowMotion == false)
        {
            StartCoroutine("StartSlowMotion");
        }
        else
        {
            StopCoroutine("StartSlowMotion");
            Time.timeScale = 1;
            slowMotionMultiplier = 1f;
            isSlowMotion = false;
        }

    }
    
    IEnumerator StartSlowMotion()
    {
        isSlowMotion = true;
        Time.timeScale = 0.5f;
        slowMotionMultiplier = 0.7f;
        yield return new WaitForSeconds(4);
        Time.timeScale = 1;
        slowMotionMultiplier = 1f;
        isSlowMotion = false;
    }
}