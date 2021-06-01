using Gamedev.UI;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamedev.Combat
{
    public class Health : MonoBehaviour
    {
        //Should only be assigned for the player!
        #region ProgrammerStuff
        [Foldout("ProgrammerStuff")]
        [InfoBox("Assign only to player!", EInfoBoxType.Warning)]
        [SerializeField] private HealthUI healthUI;
        #endregion
        [SerializeField] private GameObject deathVFX;
        [SerializeField] private int maxHealth = 300;
        [ProgressBar("Health", "maxHealth", EColor.Red)]
        [ReadOnly] public int currentHealth;

        private void Awake()
        {
            currentHealth = maxHealth;
        }
        private void Start()
        {
            if (healthUI != null) healthUI.Init(maxHealth, currentHealth);
        }
        public void TakeDamage(int damage)
        {
            //Play SFX here
            //Play animation of getting hit here
            currentHealth = Mathf.Max(currentHealth - damage, 0);
            if (healthUI != null) healthUI.UpdateHpBar(maxHealth, currentHealth);
            if (currentHealth == 0) Death();
        }
        public void Heal(int healthGained)
        {
            if (currentHealth != maxHealth) //Play SFX here
                currentHealth = Mathf.Min(currentHealth + healthGained, maxHealth);
            if (healthUI != null) healthUI.UpdateHpBar(maxHealth, currentHealth);
        }
        private void Death()
        {
            //Death anim
            GetComponentInChildren<Animator>().SetTrigger("death");
            if (deathVFX != null) Instantiate(deathVFX);
            if (gameObject.tag == "Player")
            {
                GameManager.Instance.GameOver();
            }
            //If player pause game and give game over screen
            else
            {
                Destroy(gameObject);
            }
        }
    }
}