using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorRenderer : MonoBehaviour
{

    [SerializeField] private Texture2D cursorTextureNormal;
    [SerializeField] private Texture2D cursorTextureFilled;
    private Vector2 myHotspot;


    void Start()
    {
        myHotspot = new Vector2(cursorTextureNormal.width/2, cursorTextureNormal.height/2);
    }
    void OnMouseDown()
    {
        Cursor.SetCursor(cursorTextureFilled, myHotspot, CursorMode.Auto);
    }

    private void OnMouseUp()
    {
        Cursor.SetCursor(cursorTextureNormal, myHotspot, CursorMode.Auto);

    }
}
