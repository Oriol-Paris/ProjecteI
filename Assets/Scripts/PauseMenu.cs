using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseMenu.active && Input.GetButtonDown("Cancel"))
        {
            Debug.Log("Escape pressed");
            StartCoroutine(MenuDelay());
        }
    }

    IEnumerator MenuDelay()
    {
        yield return new WaitForEndOfFrame();
        pauseMenu.SetActive(true);
    }
}
