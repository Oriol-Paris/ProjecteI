using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyDamage : MonoBehaviour
{

    public PlayerHealth playerHealth;
    public BlackOut blackOut;
    public Movement movement;
    public int damage = 0;


    private void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerHealth.TakeDamage(damage);

            //blackOut.FadeIn();

            movement.TeleportToStart();

            //blackOut.FadeOut();
        }
    }


}
