using Gamedev.Control;
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
        [Foldout("ProgrammerStuff")]
        [SerializeField] private GameObject model;
        #endregion
        [SerializeField] private GameObject deathVFX;
        [SerializeField] private int maxHealth = 300;
        [ProgressBar("Health", "maxHealth", EColor.Red)]
        [ReadOnly] public int currentHealth;

        bool canTakeDamage = true;
        bool isDead=false;
        Coroutine blinkingCoroutine;
        public bool IsDead()
        {
            return isDead;
        }
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
            if (!canTakeDamage) return;
            if (isDead) return;
            currentHealth = Mathf.Max(currentHealth - damage, 0);
            if (healthUI != null) healthUI.UpdateHpBar(maxHealth, currentHealth);
            if (currentHealth == 0) Death();
            else
            {
                if (model != null)
                {
                    blinkingCoroutine=StartCoroutine(Blinking());
                }
            }
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
            if (blinkingCoroutine!=null)
            {
                StopCoroutine(blinkingCoroutine);
            }
            if (model != null)
            {
                model.SetActive(true);
            }
            isDead = true;
            GetComponentInChildren<Animator>().SetTrigger("death");
            if (deathVFX != null) Instantiate(deathVFX);
            //If player pause game and give game over screen
            if (gameObject.tag == "Player")
            {
                GameManager.Instance.GameOver();
                Time.timeScale = 0;
            }
            else
            {
                GetComponent<Enemy>().enemyStage.OnEnemyDeath(transform.position);
                Destroy(gameObject,5f);
            }
        }

        private IEnumerator Blinking()
        {
            if (GetComponent<PlayerController>() != null) canTakeDamage = false;
            int blinkCounter = 0;
            while (blinkCounter<3)
            {
                model.SetActive(false);
                yield return new WaitForSeconds(0.2f);
                model.SetActive(true);
                yield return new WaitForSeconds(0.2f);
                blinkCounter++;
            }
            canTakeDamage = true;
        }
    }
}