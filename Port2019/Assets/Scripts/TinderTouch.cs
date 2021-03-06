﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Touch;
using System;
using DG.Tweening;
using TMPro;
using RedBlueGames.Tools.TextTyper;

public class TinderTouch : MonoBehaviour
{

    private int countChar = 0;
    public List<Sprite> Characters = new List<Sprite>();
    private Sprite currChar;


    private DateStats myDateStats;

    public TextTyper testTextTyper;
    public TextTyper NameTextTyper;
    public TextTyper LocationTextTyper;
    public TextTyper JobTextTyper;



    public TextMeshProUGUI textInfo;
    public TextMeshProUGUI textLocation;
    public TextMeshProUGUI textPlace;
    public TextMeshProUGUI textNameBig;
    public RectTransform textMatch;

    public Vector3 startPosition;
    public Vector3 resetPosition;
    private bool inputEnabled = false;

    public RectTransform YesPanel;
    public RectTransform NoPanel;

    public Transform mainImage;

    public float XThreshold;

    public float XMaxMove;
    public float screenDivider;
    public float ZMaxRotate;

    private float yPivot = 0f;

    private Vector2 startPos;
    private Vector2 currPos = new Vector2(Screen.width/2,0f);
    private Vector2 deltaMove;

    private int fingerCount = 0;
    private float tempX;
    public float speedTween;
    private float tempTime;

    public Image blackImage;
    private AudioSource audioo;
    private AudioSource audioEffects;

    public AudioClip nope;
    public AudioClip like;

    private void Awake()
    {
        yPivot = mainImage.localPosition.y;

    }
    // Start is called before the first frame update
    void Start()
    {
        //NameTextTyper.
        UtilLoadScene.Instance.SetActiveScene("Wolf");
        LeanTouch.OnFingerTap += OnFingerTap;
        LeanTouch.OnGesture += OnSwipe;
        LeanTouch.OnFingerExpired += OnFingerUp;

        XMaxMove = Screen.width / screenDivider;
        audioo = GetComponent<AudioSource>();
        audioEffects = mainImage.GetComponent<AudioSource>();
        ClosePanels();
        SetNextLook();
        blackImage.DOFade(0f, 1.5f);
        AudioManager.Instance.FadeIn(audioo);
    }

    private void OnDestroy()
    {
        LeanTouch.OnFingerTap -= OnFingerTap;
        LeanTouch.OnGesture -= OnSwipe;
        LeanTouch.OnFingerExpired -= OnFingerUp;
    }

    private void OnFingerUp(LeanFinger obj)
    {
        //if (startPos.x > XMaxMove && startPos.x < Screen.width - XMaxMove)
        //{
            ValidateOn();
        //}
        fingerCount = 0;
        OnFingerSwipe();
    }

    private void OnSwipe(List<LeanFinger> obj)
    {
        if (obj.Count == 1)
        {
            deltaMove = obj[0].ScreenPosition - startPos;
            currPos =  obj[0].ScreenPosition ;
            currPos = new Vector2(currPos.x - Screen.width / 2, currPos.y);
            OnFingerSwipe();
            //print(deltaMove);
        }
        else if (obj.Count == 0)
        {
            //currPos = Vector2.zero;
        }

        fingerCount = obj.Count;
    }

    private void OnFingerSwipe()
    {
        if (inputEnabled)
        {
            float x = currPos.x / XMaxMove;
            tempX = 0f;
            float rotateTemp = 0f;
            //float rotateTem = 0f;
            float currX = currPos.x;
            if (currX > XMaxMove)
            {
                x = XMaxMove;
                ShowMatch();
            }
            else if (currX < -XMaxMove)
            {

                x = -XMaxMove;
                ShowNext();
            }
            else
            {
                x = currX;
                ClosePanels();
            }
            if (fingerCount == 0)
            {
                print("It clears!");
                x = 0;
            }
            rotateTemp = x / XMaxMove;
            rotateTemp *= ZMaxRotate;
            //rotateTem = Mathf.Lerp(rotateTem, rotateTemp, strafeSpeed);
            DOTween.Kill(mainImage);
            //tempTime = Mathf.Abs(mainImage.eulerAngles.z - rotateTemp);
            mainImage.DORotate(new Vector3(0f, 0f, rotateTemp), speedTween);
            //mainImage.DOMoveX(x, speedTween);
            //tempX = Mathf.Lerp(tempX, x, Time.deltaTime * strafeSpeed);
            //mainImage.transform.eulerAngles = new Vector3(0f, 0f, rotateTem);
        }
    }




    private void ShowMatch()
    {
        YesPanel.gameObject.SetActive(true);
    }

    private void ShowNext()
    {
        NoPanel.gameObject.SetActive(true);
    }

    private void ClosePanels()
    {
        YesPanel.gameObject.SetActive(false);
        NoPanel.gameObject.SetActive(false);
        //textInfo.gameObject.SetActive(false);
    }

    private void ValidateOn()
    {
        float currX = currPos.x;
        if (currX > XMaxMove)
        {
            MatchWinner();
            inputEnabled = false;

            LikeSound();
        }
        else if (currX < -XMaxMove)
        {
            SetNextLook();
           
        }
        else
        {
        }
        ClosePanels();
    }

    private void MatchWinner()
    {
        textMatch.GetComponent<Image>().enabled = true;
        textMatch.GetChild(0).GetComponent<Animator>().SetTrigger("Anim");
        //TODO Anim match
        StartCoroutine(LoadNextScene());

    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(1f);
        MatchWinSound();
        DateManager.Instance.currentDate = myDateStats;
        yield return new WaitForSeconds(1.5f);
        AudioManager.Instance.FadeOut(audioo);
        blackImage.DOFade(1f, 1.5f).OnComplete(() => LoadNewScene());

    }

    private void LoadNewScene()
    {
        UtilLoadScene.Instance.SelectScene("Cutscene","work1");
    }

    private void SetNextLook()
    {
        NopeSound();
        inputEnabled = false;
        textInfo.text = "";
        textLocation.text = "";
        textPlace.text = "";
        textNameBig.text = "";
        myDateStats = DateManager.Instance.GetByIndex(countChar);
        currChar = DateManager.Instance.GetByIndex(countChar).ProfileImage;
        mainImage.GetComponent<SpriteRenderer>().sprite = currChar;
        textInfo.gameObject.SetActive(false);
        //textInfo.text = DateManager.Instance.GetByIndex(countChar).Description;
        if (countChar + 1 < DateManager.Instance.DatesCount)
        {
            countChar++;
        }
        else
        {
            countChar = 0;
        }
        mainImage.localPosition = resetPosition;
        DOTween.Kill(mainImage);
        mainImage.eulerAngles = Vector3.zero;
        mainImage.DOLocalRotate(new Vector3(0f, 360f, 0f), 1f,RotateMode.LocalAxisAdd);
        mainImage.DOLocalMove(startPosition, 1.2f).SetEase(Ease.OutExpo).OnComplete(OnSetupComplete);
    }

    private void OnSetupComplete()
    {
        inputEnabled = true;
        textInfo.gameObject.SetActive(true);
        StartCoroutine(TurnOnTexts());
    }

    private IEnumerator TurnOnTexts()
    {
        NameTextTyper.TypeText(myDateStats.Name);
        yield return new WaitForSeconds(1f);
        LocationTextTyper.TypeText(myDateStats.Distance);
        yield return new WaitForSeconds(0.4f);
        JobTextTyper.TypeText(myDateStats.JobTitle);
        yield return new WaitForSeconds(0.45f);
        testTextTyper.TypeText(myDateStats.Description);
    }

    private void OnFingerTap(LeanFinger obj)
    {
        startPos = obj.StartScreenPosition;
    }

    private void LateUpdate()
    {
        //mainImage.rectTransform.localPosition = new Vector2( tempX,yPivot);
        
    }

    IEnumerator RotateCour() 
    {
        return null;
    }
    public void MatchWinSound()
    {

    }

    public void NopeSound()
    {
        audioEffects.clip = nope;
        audioEffects.Play();
    }

    public void LikeSound()
    {
        audioEffects.clip = like;
        audioEffects.Play();
    }

    public void Quit()
    {
        GameManager.Instance.Exit();
    }
}
