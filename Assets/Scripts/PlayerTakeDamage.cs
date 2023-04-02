using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class PlayerTakeDamage : MonoBehaviour
{

    public List<string> deadlyObjects;//Lista de objetos que hacen pupa
    public int lives = 3; //Modificar desde Unity
    public Vector2 groundCheckPosition1;
    public float groundCheckRadius1 = 0.05f;
    public LayerMask lavaMask;
    public GameObject hitLava;

    //void OnTriggerEnter2D(Collider2D collision) {

    //    if (collision.gameObject.CompareTag("Lava")) {


    //    }
    //}

    void Update()
    {
        groundCheckPosition1 = new Vector2(transform.position.x, transform.position.y - 0.6f);

        Collider2D collider1 = Physics2D.OverlapCircle(groundCheckPosition1, groundCheckRadius1, lavaMask);
    }

    void OnTriggerEnter2D(Collider2D collider1)
    {
        
        if (collider1.CompareTag("Lava"))
        {
            hitLava = collider1.gameObject;
        }
        else
        {
            hitLava = null;
        }
        if (hitLava != null)
        {
            lives--;

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //Reinicia el nivel
        }

        if (lives == 0)
        {
            Debug.Log("Game Over");
        }
    }
}
