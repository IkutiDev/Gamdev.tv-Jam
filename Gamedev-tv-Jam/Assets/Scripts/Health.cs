using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth=300;
    [ProgressBar("Health", "maxHealth", EColor.Red)]
    [ReadOnly] public int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;
    }
    public void Heal(int healthGained)
    {
        currentHealth =Mathf.Min(currentHealth+healthGained,maxHealth);
    }
}
