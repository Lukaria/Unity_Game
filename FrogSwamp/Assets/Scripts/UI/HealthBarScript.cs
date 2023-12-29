using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    [SerializeField] private Slider slider;
    
    [SerializeField] private GameObject helthBarCanvas;
    
    public void SetHealth(float health)
    {
        slider.value = health;
    }

    public void SetMaxhealth(float maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }

    public void AddMaxHealth(float additional)
    {
        slider.maxValue += additional;
        slider.value += additional;
    }

    public void SubstractMaxHealth(float substraction)
    {
        slider.maxValue += substraction;
        slider.value += substraction;
    }

    public void DisableHealthBar()
    {
        if (helthBarCanvas != null)
        {
            helthBarCanvas.SetActive(false);            
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
