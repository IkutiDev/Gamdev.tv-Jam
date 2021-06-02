using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Gamedev.Combat
{
    [Serializable]
    public class PunchData
    {
        public int damage=5;
        public int energyGained=10;
    }
    public class PlayerFighter : MonoBehaviour
    {
        #region Meele Attack
        [SerializeField] private Transform leftFistTransform;
        [SerializeField] private Transform rightFistTransform;
        [SerializeField] PunchData punchOne;
        [SerializeField] PunchData punchTwo;
        [SerializeField] PunchData punchThree;
        bool canAttack = true;

        int punchState = 0;
        #endregion
        #region Range Attack
        [SerializeField] private Transform pistolEndTransform;
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private float rangeAttackCooldown;
        [SerializeField] private CooldownUI cooldownUI;

        float rangeAttackTimer = Mathf.Infinity;
        #endregion
        #region Special Attack
        [SerializeField] private int energyRequiredForSpecialAttack;
        [SerializeField] private int dmgModifier=2;
        [SerializeField] private float dmgIncreaseTime=5f;
        [SerializeField] private Color specialColor;
        private Color normalColor;
        bool canSpecialAttack = true;
        bool increaseDamage = false;
        #endregion
        #region Jump Attack
        [SerializeField] PunchData jumpingPunch;
        bool canJumpAttack = true;
        #endregion
        [SerializeField] private GameObject modelParent;
        [SerializeField] private SkinnedMeshRenderer model;
        Animator animator;



        

        private void Start()
        {
            animator = GetComponentInChildren<Animator>();
            normalColor = model.materials[1].color;
        }
        private void Update()
        {
            rangeAttackTimer += Time.deltaTime;
            cooldownUI.UpdateTimer((int)Mathf.Min(rangeAttackTimer, rangeAttackCooldown));
        }
        public void BasicAttack()
        {
            if (canAttack)
            {
                canAttack = false;
                punchState++;
                animator.SetInteger("punch", punchState);
            }
        }
        public void JumpAttack()
        {
            if (canJumpAttack)
            {
                canJumpAttack = false;
                animator.SetTrigger("jumpingAttack");
            }
        }
        private void Next()
        {
            canAttack = true;
        }

        private void Stop()
        {
            punchState = 0;
            animator.SetInteger("punch", punchState);
            canAttack = true;
        }
        public void RangeAttack()
        {
            if (rangeAttackTimer > rangeAttackCooldown)
            {
                rangeAttackTimer = 0f;
                animator.SetTrigger("rangeAttack");
            }
        }
        public void RangeAttackAnimTrigger()
        {
            var projectile = Instantiate(projectilePrefab, pistolEndTransform.position, projectilePrefab.transform.rotation);
            projectile.GetComponent<Projectile>().goLeft = modelParent.transform.localRotation.eulerAngles.y >= 180f;
            if (increaseDamage) projectile.GetComponent<Projectile>().damage *= projectile.GetComponent<Projectile>().damage * dmgModifier;
        }
        public void SpecialAttack()
        {
            if (canSpecialAttack)
            {
                if (GetComponent<Energy>().GetCurrentEnergy() >= energyRequiredForSpecialAttack)
                {
                    canSpecialAttack = false;
                    StartCoroutine(IncreaseDamage());
                    animator.SetTrigger("specialAttack");
                    GetComponent<Energy>().IncreaseEnergy(-energyRequiredForSpecialAttack);
                }
            }
        }
        private IEnumerator IncreaseDamage()
        {
            model.materials[1].color = specialColor;
            increaseDamage = true;
            yield return new WaitForSeconds(dmgIncreaseTime);
            increaseDamage = false;
            canSpecialAttack = true;
            model.materials[1].color = normalColor;
        }
        public void Punch_Impact(int punchState)
        {
            Vector3 fistPosition;
            int energyGained;
            int damage;
            switch (punchState)
            {
                case 1:
                    fistPosition = leftFistTransform.position;
                    damage = punchOne.damage;
                    energyGained = punchOne.energyGained;
                    break;
                case 2:
                    fistPosition = rightFistTransform.position;
                    damage = punchTwo.damage;
                    energyGained = punchTwo.energyGained;
                    break;
                case 3:
                    fistPosition = rightFistTransform.position;
                    damage = punchThree.damage;
                    energyGained = punchThree.energyGained;
                    break;
                case -1:
                    fistPosition = rightFistTransform.position;
                    damage = jumpingPunch.damage;
                    energyGained = jumpingPunch.energyGained;
                    break;
                default:
                    Debug.LogError("WTF?");
                    return;
            }
            if (increaseDamage) damage = damage * dmgModifier;
            var targets =Physics.SphereCastAll(fistPosition, .7f,Vector3.forward);
            foreach(var target in targets)
            {
                if (target.collider == null) continue;
                if (target.collider.GetComponent<Health>() == null) continue;
                if (target.collider.tag == "Player") continue;

                target.collider.GetComponent<Health>().TakeDamage(damage);
                GetComponent<Energy>().IncreaseEnergy(energyGained);
            }
            if (punchState == -1) return;
            Next();

        }
        public void Punch_Finish(int punchState)
        {
            Stop();
        }
        public void JumpingAttack_Finish()
        {
            canJumpAttack = true;
        }
    }
}