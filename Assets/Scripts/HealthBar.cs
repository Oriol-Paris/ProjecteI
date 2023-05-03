using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public PlayerHealth playerHealth;
    public Slider mySlider;
    public Gradient myGradient;
    public Image fill;
    // Start is called before the first frame update
    void Start()
    {
        mySlider.maxValue = playerHealth.maxHealth;
        mySlider.value = playerHealth.health;

        fill.color = myGradient.Evaluate(1f);
    }

    // Update is called once per frame
    void Update()
    {
        mySlider.value = playerHealth.health;
        fill.color = myGradient.Evaluate(mySlider.normalizedValue);
    }
}
