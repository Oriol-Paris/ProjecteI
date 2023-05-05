using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExitPauseMenu : MonoBehaviour
{
    public Canvas myCanvas;
    public GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        myCanvas = GetComponent<Canvas>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (pauseMenu.active && Input.GetButtonDown("Cancel"))
        {
            Debug.Log("Escape pressed");
            pauseMenu.SetActive(false);
        }
    }

    public void ExitMenu()
    {
        //myCanvas.enabled = false;
        pauseMenu.SetActive(false);
    }
}
