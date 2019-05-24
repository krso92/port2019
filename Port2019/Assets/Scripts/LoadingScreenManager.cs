using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LoadingScreenManager : MonoBehaviour
{

    public Image blackImage;
    public float sceneLength;
    private AudioSource audioo;

    // Start is called before the first frame update
    void Start()
    {
        audioo = GetComponent<AudioSource>();
        blackImage.DOFade(0f, 1.5f);
        AudioManager.Instance.FadeIn(audioo);
        StartCoroutine(WaitForFadeOut());
    }



    public IEnumerator WaitForFadeOut()
    {
        yield return new WaitForSeconds(sceneLength);
        AudioManager.Instance.FadeOut(audioo);
        yield return new WaitForSeconds(0.5f);
        blackImage.DOFade(1f, 1.5f).OnComplete(() =>LoadNewScene());

    }

    private void LoadNewScene()
    {
        print("tries to loead new scene1");
        UtilLoadScene.Instance.SelectScene(UtilLoadScene.Instance.nextSceneToLoad);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
