using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    public static UIManager Instance
    {
        get
        {
            return instance;
        }
    }

    [SerializeField]
    private Image profileImage;

    public Image blackImage;

    public CanvasGroup bubble;
    public TextMeshProUGUI text;

    [SerializeField]
    private TextMeshProUGUI timerText;

    [SerializeField]
    private CanvasGroup scoreGroup;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    public void SetTimerText(string text)
    {
        timerText.text = text;
    }

    public void SetScoreText(string text)
    {
        scoreGroup.DOFade(1f, 1f);
        scoreText.text = text;
        Invoke("HideScoreText", 5f);
    }

    public void HideScoreText()
    {
        scoreGroup.DOFade(0f, 1f);
    }

    private void Awake()
    {
        instance = this;
    }

    private void OnDestroy()
    {
        instance = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        profileImage.sprite = DateManager.Instance.currentDate.GameImage;
        blackImage.DOFade(0f, 3f);
        //bubble.DOFade(0f, 0f);
        scoreGroup.DOFade(0f, 0f);
    }
    
    public void ShowBubble(string text, bool forceHideProfileImage = false, bool delayWithBubble = false)
    {
        if (delayWithBubble)
        {
            bubble.DOFade(0f, 0f);
            Debug.Log("kako valja");
            Invoke("BubbleFadeIn", 3f);
        }
        else
        {
            BubbleFadeIn();
        }
        this.text.text = text;
        Invoke("HideBubble", 12f);
        if (forceHideProfileImage)
        {
            Invoke("HideProfileImage", 12f);
        }
    }

    private void BubbleFadeIn()
    {
        bubble.DOFade(1f, 1f);
    }

    public void HideCounterText()
    {
        timerText.DOFade(0f, 1f);
    }

    private void HideProfileImage()
    {
        profileImage.DOFade(0f, 1.5f);
    }

    public void HideBubble()
    {
        bubble.DOFade(0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackToTinder() 
    {
        blackImage.raycastTarget = true;
        blackImage.DOFade(1f, 1.5f).OnComplete(() => LoadNewScene());
        AudioManager.Instance.FadeOutMusicians();
    }

    private void LoadNewScene()
    {
        UtilLoadScene.Instance.SelectScene("LoadingScene","Wolf");

    }
}
