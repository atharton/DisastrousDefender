using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] float maxHealth = 100;
    [SerializeField] float currentHealth = 100; // REMOVE SERIALIZE LATER

    public void ResetHealth(float maxHealth)
    {
        setMaxHealth(maxHealth);
        setCurrentHealth(maxHealth);
    }

    public void reduceHealth(float damage)
    {
        if (currentHealth - damage <= 0) currentHealth = 0;
        else currentHealth -= damage;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }
    public float GetMaxHealth()
    {
        return maxHealth;
    }

    // PRIVATE METHODS

    private void setMaxHealth(float maxHealth)
    {
        if (maxHealth > 0) this.maxHealth = maxHealth;
    }
    private void setCurrentHealth(float currentHealth)
    {
        if (currentHealth > 0) this.currentHealth = currentHealth;
        if (maxHealth < currentHealth) setMaxHealth(currentHealth);
    }
}
