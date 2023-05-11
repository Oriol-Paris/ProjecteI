using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorRenderer : MonoBehaviour
{

    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private Texture2D cursorTextureNormal;
    private Vector2 myHotspot;


    void Start()
    {
        myHotspot = new Vector2(cursorTexture.width/2, cursorTexture.height/2);
    }
    private void OnMouseOver()
    {
        Cursor.SetCursor(cursorTexture, myHotspot, CursorMode.Auto);
    }
    private void OnMouseExit()
    {
        Cursor.SetCursor(cursorTextureNormal, myHotspot, CursorMode.Auto);
    }
}
