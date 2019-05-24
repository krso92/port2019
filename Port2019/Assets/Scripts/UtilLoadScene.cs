using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UtilLoadScene : TGlobalSingleton<UtilLoadScene>
{

    public string activeScene;
    public string sceneLoad;
    private Scene loadingScene;
    private bool isLoading;
    public string nextSceneToLoad = null;

    public void SetActiveScene(string scene)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(scene));
    }

    public void SelectScene(string sceneToLoad)
    {

        StartCoroutine(FadeOutAndIn(sceneToLoad));
        //loadingScene = SceneManager.GetActiveScene();
        activeScene = sceneToLoad;
        //UnloadScene();

        //set the active scene to the next scene
    }

    public void SelectScene(string sceneToLoad,string nextScene)
    {
        nextSceneToLoad = nextScene;
        StartCoroutine(FadeOutAndIn(sceneToLoad));
        //loadingScene = SceneManager.GetActiveScene();
        activeScene = sceneToLoad;
        //UnloadScene();

        //set the active scene to the next scene
    }



    public void UnloadScene()
    {
        if (isLoading)
        {
            isLoading = false;
            //fadeOverlay.FadeOut();
            StartCoroutine(UnloadCurrentScene());
        }
    }

    IEnumerator FadeOutAndIn(string sceneToLoad)
    {

        //wait until the fade image is entirely black (alpha=1) then load next scene
        //yield return new WaitUntil(() => fadeImage.color.a == 1);
        isLoading = true;
        AsyncOperation _async = new AsyncOperation();
        _async = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Single);
        loadingScene = SceneManager.GetSceneByName(sceneToLoad);
        Debug.Log("loading scene:" + loadingScene.name);
        while (!_async.isDone)
        {
            yield return null;
        }
        isLoading = false;

    }

    IEnumerator UnloadCurrentScene()
    {

        AsyncOperation _async = new AsyncOperation();
        _async = SceneManager.UnloadSceneAsync(loadingScene);

        while (!_async.isDone)
        {
            yield return null;
        }


    }

}
