using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Touch;
using System;
using DG.Tweening;

public class TinderTouch : MonoBehaviour
{

    public List<Transform> Characters = new List<Transform>();

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

    private void Awake()
    {
        yPivot = mainImage.localPosition.y;

    }
    // Start is called before the first frame update
    void Start()
    {

        LeanTouch.OnFingerTap += OnFingerTap;
        LeanTouch.OnGesture += OnSwipe;
        LeanTouch.OnFingerExpired += OnFingerUp;

        XMaxMove = Screen.width / screenDivider;
    }

    private void OnFingerUp(LeanFinger obj)
    {
        fingerCount = 0;
        OnFingerSwipe();
        ValidateOn();
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
        mainImage.DORotate(new Vector3(0f, 0f, rotateTemp),speedTween);
        //mainImage.DOMoveX(x, speedTween);
        //tempX = Mathf.Lerp(tempX, x, Time.deltaTime * strafeSpeed);
        //mainImage.transform.eulerAngles = new Vector3(0f, 0f, rotateTem);
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
    }

    private void ValidateOn()
    {
        float currX = currPos.x;
        if (currX > XMaxMove)
        {
            print("Its a match");
        }
        else if (currX < -XMaxMove)
        {
            print("Its next");
        }
        else
        {
        }
        ClosePanels();
    }

    private void OnFingerTap(LeanFinger obj)
    {
        startPos = obj.StartScreenPosition;
    }





    // Update is called once per frame
    void Update()
    {
       
    }

    private void LateUpdate()
    {
        //mainImage.rectTransform.localPosition = new Vector2( tempX,yPivot);
        
    }

    IEnumerator RotateCour() 
    {
        return null;
    }

}
