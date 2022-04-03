using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    private Slider healthSlider;
    // Start is called before the first frame update
    void Start()
    {
        healthSlider = GetComponent<Slider>(); 
    }

    public void SetMaxHealth(float health)
    {
        healthSlider.maxValue = health;
    }

    public void DisplayHealth(float health)
    {
        healthSlider.value = health;
    }

}
