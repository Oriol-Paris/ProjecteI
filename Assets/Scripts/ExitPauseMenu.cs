using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExitPauseMenu : MonoBehaviour
{
    public Canvas myCanvas;

    // Start is called before the first frame update
    void Start()
    {
        myCanvas = GetComponent<Canvas>();
    }

    // Update is called once per frame

    public void ExitMenu()
    {
        myCanvas.enabled = false;
    }
}
