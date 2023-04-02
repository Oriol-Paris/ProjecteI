using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int health;
    public int maxHealth = 10;
    public Movement movement;
    //public BlackOut blackOut;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;

        if (amount > 1)
        {
            movement.TeleportToStart();

            //blackOut.FadeIn();
            //blackOut.FadeOut();
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    //Implementar tiempo de invulneravilidad (pre-alpha)
}
