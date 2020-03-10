using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartScene : MonoBehaviour
{
    public delegate void Change();
    public static event Change TimeChanged;
    private void OnEnable()
    {
        
        Scene startScene = SceneManager.GetSceneByBuildIndex(0);
        Scene thisScene = SceneManager.GetSceneByBuildIndex(1);
        SceneManager.SetActiveScene(thisScene);
    }
}
