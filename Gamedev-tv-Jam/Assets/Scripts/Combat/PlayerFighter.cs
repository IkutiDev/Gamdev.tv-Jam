using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Gamedev.Combat
{
    public class PlayerFighter : MonoBehaviour
    {
        //[SerializeField] private 
        [SerializeField] private float rangeAttackCooldown;
        [SerializeField] private int energyRequiredForSpecialAttack;
        [SerializeField] private AnimationClip punchOneAnimation;
        [SerializeField] private AnimationClip punchTwoAnimation;
        [SerializeField] private AnimationClip punchThreeAnimation;
        [SerializeField] private Transform fistTransform;
        [SerializeField] private Transform pistolEndTransform;
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private CooldownUI cooldownUI;
        [SerializeField] private GameObject model;
        Animator animator;
        bool punchOneImpact;
        bool punchOne=false;
        bool punchTwo = false;
        bool punchThree = false;

        float rangeAttackTimer = Mathf.Infinity;

        private void Start()
        {
            animator = GetComponentInChildren<Animator>();
        }
        private void Update()
        {
            rangeAttackTimer += Time.deltaTime;
            cooldownUI.UpdateTimer((int)Mathf.Min(rangeAttackTimer, rangeAttackCooldown));
        }
        public void BasicAttack()
        {
            if (punchThree ==false && punchTwo == false &&punchOne==false)
            {
                punchOne = true;
                animator.SetBool("punchOne", punchOne);
                Invoke("DisablePunch1", punchOneAnimation.length);
            }
            else if(punchTwo == false && punchThree == false)
            {
                punchTwo = true;
                animator.SetBool("punchTwo", punchTwo);
                Invoke("DisablePunch2", punchTwoAnimation.length);
            }
            else if(punchThree == false)
            {
                punchThree = true;
                animator.SetBool("punchThree", punchThree);
                Invoke("DisablePunch3", punchThreeAnimation.length);
            }
        }
        public void RangeAttack()
        {
            if (rangeAttackTimer > rangeAttackCooldown)
            {
                rangeAttackTimer = 0f;
                animator.SetTrigger("rangeAttack");
            }
            //Do range attack
        }
        public void SpecialAttack()
        {
            if (GetComponent<Energy>().GetCurrentEnergy() >= energyRequiredForSpecialAttack)
            {
                //Do special attack
                animator.SetTrigger("specialAttack");
                GetComponent<Energy>().IncreaseEnergy(-energyRequiredForSpecialAttack);
            }
        }
        private void DisablePunch1()
        {
            punchOne = false;
            animator.SetBool("punchOne", punchOne);
        }
        private void DisablePunch2()
        {
            punchTwo = false;
            animator.SetBool("punchTwo", punchTwo);
        }
        private void DisablePunch3()
        {
            punchThree = false;
            animator.SetBool("punchThree", punchThree);
        }
        public void Punch_1_Impact()
        {
            Debug.Log("Punch Impact");
            var targets =Physics.SphereCastAll(fistTransform.position, .4f,Vector3.forward);
            foreach(var target in targets)
            {
                if (target.collider == null) continue;
                if (target.collider.GetComponent<Health>() == null) continue;
                if (target.collider.tag == "Player") continue;

                target.collider.GetComponent<Health>().TakeDamage(10);
                GetComponent<Energy>().IncreaseEnergy(5);
            }
            punchOneImpact = true;

        }
        public void Punch_1_Finish()
        {
            punchOneImpact = false;
        }
        public void RangeAttackAnimTrigger()
        {
            var projectile=Instantiate(projectilePrefab, pistolEndTransform.position, projectilePrefab.transform.rotation);
            projectile.GetComponent<Projectile>().goLeft = model.transform.localRotation.eulerAngles.y>=180f;
        }
        private void OnDrawGizmos()
        {
            if (punchOneImpact)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(fistTransform.position, .4f);
            }
        }
    }
}