using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ResolutionManager : TGlobalSingleton<ResolutionManager> {


    // Fixed aspect ratio parameters
    static public bool FixedAspectRatio = true;
    static public float TargetAspectRatio = 16f/9f;

    // Windowed aspect ratio when FixedAspectRatio is false
    static public float WindowedAspectRatio = 16f/9f;
    int curreResIndex = 0;
    // List of horizontal resolutions to include
    int[] resolutions = new int[] { 1280, 1400, 1600, 1920 };

    public Resolution DisplayResolution;
    public List<Vector2> WindowedResolutions, FullscreenResolutions;

    int currWindowedRes, currFullscreenRes;

    public bool isFullscreen;

    //void Start()
    //{
    //    StartCoroutine(StartRoutine());
    //}

    public void InitSetup()
    {
       // TargetAspectRatio = (float)Screen.width / (float)Screen.height;
        StartCoroutine(StartRoutine());

    }

    private IEnumerator StartRoutine()
    {
#if RESOLUTION
        if (Application.platform == RuntimePlatform.OSXPlayer)
        {
            DisplayResolution = Screen.currentResolution;
        }
        else
        {
            if (Screen.fullScreen)
            {
                Resolution r = Screen.currentResolution;
                Screen.fullScreen = false;

                yield return null;

                DisplayResolution = Screen.currentResolution;
                if (QnGlobals.Instance.windowResIndex == -1 && QnGlobals.Instance.fullscreenResIndex == -1)
                {
                    int height = (int)(1280f / TargetAspectRatio);
                    Screen.SetResolution(1280,height, true, 60);
                }
                
                yield return new WaitForSeconds(3f);

                Debug.LogError("current resolution " + r.width + " " + r.height);

                yield return null;
                Screen.fullScreen = true;
            }
            else
            {
                DisplayResolution = Screen.currentResolution;
            }
        }
        InitResolutions(); 
#endif
        yield return null;
        //if (Screen.fullScreen)  SetResolution(3, true);
        //else SetResolution(3, false);

    }

    private void InitResolutions()
    {
        float screenAspect = (float)DisplayResolution.width / DisplayResolution.height;
        WindowedResolutions = new List<Vector2>();
        FullscreenResolutions = new List<Vector2>();

        foreach (int w in resolutions)
        {

            // Adding resolution only if it's 20% smaller than the screen
            //if (w < DisplayResolution.width * 0.8f)
            //{
            Vector2 windowedResolution = new Vector2(w, Mathf.Round(w / (FixedAspectRatio ? TargetAspectRatio : WindowedAspectRatio)));

            WindowedResolutions.Add(windowedResolution);

            FullscreenResolutions.Add(new Vector2(w, Mathf.Round(w / screenAspect)));
            //  }
        }

        // Adding fullscreen native resolution
        FullscreenResolutions.Add(new Vector2(DisplayResolution.width, DisplayResolution.height));

        // Adding half fullscreen native resolution
        Vector2 halfNative = new Vector2(DisplayResolution.width * 0.5f, DisplayResolution.height * 0.5f);
        if (halfNative.x > resolutions[0] && FullscreenResolutions.IndexOf(halfNative) == -1)
            FullscreenResolutions.Add(halfNative);

        FullscreenResolutions = FullscreenResolutions.OrderBy(resolution => resolution.x).ToList();

        bool found = false;

        if (Screen.fullScreen)
        {
            currWindowedRes = WindowedResolutions.Count - 1;

            for (int i = 0; i < FullscreenResolutions.Count; i++)
            {
                if (FullscreenResolutions[i].x == Screen.width && FullscreenResolutions[i].y == Screen.height)
                {
                    currFullscreenRes = i;
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                SetResolution(FullscreenResolutions.Count - 2, true);
                curreResIndex = FullscreenResolutions.Count - 2;
            }

            //if (QnGlobals.Instance.windowResIndex != -1 && !QnGlobals.Instance.isFullscreen)
            //{
            //    SetResolution(QnGlobals.Instance.windowResIndex, false);
            //}
            //else if (QnGlobals.Instance.fullscreenResIndex != -1 && QnGlobals.Instance.isFullscreen)
            //{
            //    SetResolution(QnGlobals.Instance.fullscreenResIndex, true);
            //}
            //else
            //{
                int height = (int)(1280f / TargetAspectRatio);
                Screen.SetResolution(1280, height, true, 60);
            //}
         

        }
        else
        {
            currFullscreenRes = FullscreenResolutions.Count - 1;

            for (int i = 0; i < WindowedResolutions.Count; i++)
            {
                if (WindowedResolutions[i].x == Screen.width && WindowedResolutions[i].y == Screen.height)
                {
                    found = true;
                    currWindowedRes = i;
                    break;
                }
            }

            if (!found)
                SetResolution(WindowedResolutions.Count - 1, false);

            isFullscreen = Screen.fullScreen;
        }
    }

    public void SetResolutionFromApp ()
    {
        int index = 0;
        bool fullScreen = Screen.fullScreen;
        if (curreResIndex != 0)
        {
            index = curreResIndex;
            SetResolution(index, fullScreen);
        }
    }

    public void SetResolution(int index, bool fullscreen, bool fromApplication = false)
    {
        StartCoroutine(SetRes(index, fullscreen, fromApplication));

        
    }

    public void UpdateQualitySettings()
    {
        //Debug.LogError("quality index " + QnGlobals.Instance.qualityIndex);
        //if (QnGlobals.Instance.qualityIndex != -1)
        //{
        //    QnHudController.Instance.options.SetQualityLevel(QnGlobals.Instance.qualityIndex);
        //}
    }

    IEnumerator SetRes(int index, bool fullscreen, bool fromApplication = false)
    {
        Vector2 r = new Vector2();
        if (fullscreen)
        {
            currFullscreenRes = index;
            r = FullscreenResolutions[currFullscreenRes];
        }
        else
        {
            currWindowedRes = index;
            r = WindowedResolutions[currWindowedRes];
        }

        //QnGlobals.Instance.isFullscreen = fullscreen;
        bool fullscreen2windowed = Screen.fullScreen & !fullscreen;

        Screen.fullScreen = false;

        Debug.LogError("Setting resolution to " + (int)r.x + "x" + (int)r.y);
        Screen.SetResolution((int)r.x, (int)r.y, fullscreen);
        yield return new WaitForSeconds(0.1f);

        if (fullscreen)
        {
            Screen.fullScreen = true;
            isFullscreen = true;
        }
        else
        {
            isFullscreen = false;
        }

        // On OSX the application will pass from fullscreen to windowed with an animated transition of a couple of seconds.
        // After this transition, the first time you exit fullscreen you have to call SetResolution again to ensure that the window is resized correctly.
        if (Application.platform == RuntimePlatform.OSXPlayer)
        {
            // Ensure that there is no SetResolutionAfterResize coroutine running and waiting for screen size changes
            StopAllCoroutines();

            // Resize the window again after the end of the resize transition
            if (fullscreen2windowed) StartCoroutine(SetResolutionAfterResize(r));
        }
    }

    public void RefreshResolution()
    {
        if (Screen.fullScreen)
            SetResolution(currFullscreenRes, true);
        else SetResolution(currWindowedRes, true);
    }

    private IEnumerator SetResolutionAfterResize(Vector2 r)
    {
        int maxTime = 5; // Max wait for the end of the resize transition
        float time = Time.time;

        // Skipping a couple of frames during which the screen size will change
        yield return null;
        yield return null;

        int lastW = Screen.width;
        int lastH = Screen.height;

        // Waiting for another screen size change at the end of the transition animation
        while (Time.time - time < maxTime)
        {
            if (lastW != Screen.width || lastH != Screen.height)
            {
                Debug.Log("Resize! " + Screen.width + "x" + Screen.height);

                yield return new WaitForSeconds(1f);
                Screen.SetResolution((int)r.x, (int)r.y, Screen.fullScreen);

                yield return new WaitForSeconds(3f);

                yield break;
            }

            yield return null;
        }
    }

    public void ToggleFullscreen()
    {
        if (Screen.fullScreen)
        {
            for (int i = 0; i < WindowedResolutions.Count; i++)
            {
                if (FullscreenResolutions[currFullscreenRes] == WindowedResolutions[i])
                {
                    currWindowedRes = i;
                    continue;
                }
            }
        }
        else
        {
            for (int i = 0; i < FullscreenResolutions.Count; i++)
            {
                if (WindowedResolutions[currWindowedRes] == FullscreenResolutions[i])
                {
                    currFullscreenRes = i;
                    continue;
                }
            }

        }
        //QnGlobals.Instance.isFullscreen = Screen.fullScreen;

        SetResolution(
            Screen.fullScreen ? currWindowedRes : currFullscreenRes,
            !Screen.fullScreen);
        //QnHudController.Instance.options.fullscreenToggle.Toggle(Screen.fullScreen);
        
    }
    
}
