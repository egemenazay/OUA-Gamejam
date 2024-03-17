using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    public Slider healthSlider;
    private Player health;
    public int maxHealth = 100;
    

    private void Start()
    {
        health = FindObjectOfType<Player>();
        Debug.Log(health.hp);
        healthSlider.maxValue = maxHealth;
    }

    private void Update()
    {
        if (healthSlider.value != health.hp)
        {
            healthSlider.value = health.hp;
        }
    }
}

