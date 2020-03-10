using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class AlphaLoader : MonoBehaviour
{
    [SerializeField] Text loadingText;
    [SerializeField] GameObject introObj;
    int levelIndex = 1;
    private void Start()
    {
        StartCoroutine("LoadScene");
        Application.backgroundLoadingPriority = ThreadPriority.High;
        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "NewHangar")
        {
            introObj.SetActive(false);
        }
    }

    IEnumerator LoadScene()
    {
        AsyncOperation loadAsync = SceneManager.LoadSceneAsync(levelIndex, LoadSceneMode.Additive);
        loadAsync.allowSceneActivation = false;
        while (loadAsync.progress < 0.9f)
        {
            loadingText.text = "LOADING....    %" + (int)(loadAsync.progress * 100) + "   " + levelIndex + "/" + SceneManager.sceneCountInBuildSettings;

            yield return null;
        }

        loadAsync.allowSceneActivation = true;
        LoadLevelsAsync();
    }


    private void LoadLevelsAsync()
    {
        if (levelIndex < SceneManager.sceneCountInBuildSettings)
        {
            levelIndex++;
            StartCoroutine("LoadScene");
        }
        else
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("NewHangar"));
        }
    }





}
