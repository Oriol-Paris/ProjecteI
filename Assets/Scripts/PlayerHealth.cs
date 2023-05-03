using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int health;
    public int maxHealth = 10;
<<<<<<< Updated upstream
=======
    public Movement movement;
    private bool isInvincible = false;
    public float invincibilitySeconds = 1f;
    //public BlackOut blackOut;
>>>>>>> Stashed changes

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (isInvincible) 
        {
            return;
        }

        health -= amount;
        if(health <= 0)
        {
            Destroy(gameObject);
        }

        StartCoroutine(InvincibilityOnHit());
    }
<<<<<<< Updated upstream
=======

    private IEnumerator InvincibilityOnHit()
    {
        Debug.Log("Player Turned Invincible!");
        isInvincible = true;

        yield return new WaitForSeconds(invincibilitySeconds);

        isInvincible = false;
        Debug.Log("Player is no longer invincible!");
    }
>>>>>>> Stashed changes
}
