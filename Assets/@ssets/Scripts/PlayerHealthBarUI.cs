using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBarUI : MonoBehaviour
{
    [SerializeField] private Slider healthBarSlider;

    private float maxHealth;
    private float currentHealth;

    private void Start()
    {
        InitiateHealthBar();
    }

    private void InitiateHealthBar()
    {
        healthBarSlider.maxValue = maxHealth;
        healthBarSlider.value = maxHealth;
    }

    public void DecreaseHealth(float damageAmount)
    {
        healthBarSlider.value -= damageAmount;
    }

    public void IncreaseHealth(float healAmount)
    {
        healthBarSlider.value += healAmount;
    }
}
