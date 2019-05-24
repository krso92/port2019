using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : TGlobalSingleton<CursorManager>
{
    public Texture2D defCursor;
    public Texture2D newCursor;

    private Vector2 hotSpot = Vector2.zero;
    private CursorMode mode = CursorMode.ForceSoftware;


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SetElementCursor()
    {
        Cursor.SetCursor(newCursor, hotSpot, mode);
    }

    public void SetNormalCursor()
    {
        Cursor.SetCursor(defCursor, hotSpot, mode);
    }

}
