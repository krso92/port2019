using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Image profileImage;

    public Image blackImage;




    // Start is called before the first frame update
    void Start()
    {
        profileImage.sprite = DateManager.Instance.currentDate.GameImage;
        blackImage.DOFade(0f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackToTinder() 
    {
        blackImage.DOFade(1f, 1.5f).OnComplete(() => LoadNewScene());

    }

    private void LoadNewScene()
    {
        UtilLoadScene.Instance.SelectScene("LoadingScene","Wolf");

    }
}
