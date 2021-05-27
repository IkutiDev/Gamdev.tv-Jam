using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    //Should only be assigned for the player!
    [InfoBox("Assign only to player!", EInfoBoxType.Warning)]
    [SerializeField] private HealthUI healthUI;
    [SerializeField] private int maxHealth=300;
    [ProgressBar("Health", "maxHealth", EColor.Red)]
    [ReadOnly] public int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }
    private void Start()
    {
        if(healthUI!=null)healthUI.Init(maxHealth,currentHealth);
    }
    public void TakeDamage(int damage)
    {
        //Play SFX here
        //Play animation of getting hit here
        currentHealth = Mathf.Max(currentHealth - damage,0);
        if (healthUI != null) healthUI.UpdateHpBar(maxHealth, currentHealth);
    }
    public void Heal(int healthGained)
    {
        if(currentHealth!=maxHealth) //Play SFX here
        currentHealth =Mathf.Min(currentHealth+healthGained,maxHealth);
        if (healthUI != null) healthUI.UpdateHpBar(maxHealth, currentHealth);
    }
}
