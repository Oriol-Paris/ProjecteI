using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public Canvas myCanvas;
    public GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        myCanvas = GetComponent<Canvas>();

        //myCanvas.enabled = false;
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseMenu.active && Input.GetButtonDown("Cancel"))
        {
            Debug.Log("Escape pressed");
            //myCanvas.enabled = true;
            StartCoroutine(MenuDelay());
        }
    }

    IEnumerator MenuDelay()
    {
        yield return new WaitForEndOfFrame();
        pauseMenu.SetActive(true);
    }
}
