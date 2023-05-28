using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExitPauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

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
        pauseMenu.SetActive(false);
    }
}
