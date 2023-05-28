using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    public int health;
    public int maxHealth = 10;
    public Movement movement;
    private bool isInvincible = false;
    public float invincibilitySeconds = 1f;
    public Animator myAnimator;
    private int dmgAmount;
    //public BlackOut blackOut;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        myAnimator = GetComponent<Animator>();
    }

    public void TakeDamage(int amount)
    {
        if (isInvincible)
        {
            return;
        }

        health -= amount;
        myAnimator.SetBool("DamageTaken", true);

        if (health <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("HUB");
        }

        dmgAmount = amount;

        StartCoroutine(InvincibilityOnHit());
    }

    private IEnumerator InvincibilityOnHit()
    {
        Debug.Log("Player Turned Invincible!");
        isInvincible = true;

        if (dmgAmount == 2)
            movement.TeleportToStart();

        yield return new WaitForSeconds(invincibilitySeconds);

        isInvincible = false;
        Debug.Log("Player is no longer invincible!");
        myAnimator.SetBool("DamageTaken", false);
    }
}
